using AutoMapper;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Booking.Models.DTOs.Image;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        public ImageService(ApplicationDbContext context, IMapper mapper, IRoomService roomService)
        {
            _context = context;
            _mapper = mapper;
            _roomService = roomService;


        }
        public async Task<ResponseC> DeleteAsync(int id, int userId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(x => x.Id == id && x.CreatorId == userId);
            if (image == null) return new ResponseC(false, "Image not found");
            image.status = Enums.Status.Deleted;
            await _context.SaveChangesAsync();
            return new ResponseC(true, "Image deleted");


        }

        public async Task<ResponseT<List<ImageDTO>>> GetAllAsync(int userId)
        {
            var images = await _context.Images.ToListAsync();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status == Enums.Status.Active);
            if (user != null && user.Role == Enums.Role.Hoteladmin)
            {
                images = images.Where(x => x.CreatorId == userId).ToList();
            }
            if (user == null || user.Role != Enums.Role.SuperAdmin)
            {
                images = images.Where(x => x.status == Enums.Status.Active).ToList();
            }

            var dtos = new List<ImageDTO>();

            foreach (var image in images)
            {
                dtos.Add(_mapper.Map<ImageDTO>(image));
            }
            return new ResponseT<List<ImageDTO>>(true, dtos, "");
        }

        public async Task<ResponseT<ImageDTO>> GetByIdAsync(int id, int userId)
        {
            var image = await GetAllAsync(userId);
            if (!image.Success) return new ResponseT<ImageDTO>(false, null, image.Message);
            var imageDTO = image.Data.FirstOrDefault(x => x.Id == id);
            if (imageDTO == null) return new ResponseT<ImageDTO>(false, null, "Image not found");
            return new ResponseT<ImageDTO>(true, imageDTO, "");
        }

        public async Task<ResponseC> AddAsync(AddImageDTO dto, int userId)
        {
            var room = await _roomService.GetByIdAsync(dto.RoomId, userId);
            if (!room.Success) return new ResponseC(false, "RoomNotFound");

            var exist = await _context.Images.AnyAsync(x => x.source == dto.Source && x.roomId == room.Data.Id);

            if (exist) return new ResponseC(false, "Image Already exists");
            var image = _mapper.Map<Image>(dto);
            image.CreatorId = userId;

            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "image uploaded");
        }
    }
}
