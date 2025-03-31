using AutoMapper;
using Azure;
using Booking.Data;
using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Hotel;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.EntityFrameworkCore;
namespace Booking.Services
{
    public class HotelService : IHotelService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseC> CreateHotelAsync(AddHotelDTO dto, int adminId)
        {

            var hotel = _mapper.Map<Hotel>(dto);
            hotel.CreatorId = adminId;
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "hotel added successfully");




        }
        public async Task<ResponseT<List<HotelDTO>>> GetAllAsync(int UserId)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId && x.Status == Enums.Status.Active);
            var hotels = await _context.Hotels.ToListAsync();
            if (user != null && hotels != null && user.Role == Enums.Role.Hoteladmin)
            {
                hotels = hotels.Where(x => x.CreatorId == user.Id).ToList();
            }
            if ((user == null || user.Role != Enums.Role.SuperAdmin) && hotels != null)
            {
                hotels = hotels.Where(x => x.status == Enums.Status.Active).ToList();
            }



            var h = new List<HotelDTO>();
            if (hotels != null) hotels.ForEach(x => h.Add(_mapper.Map<HotelDTO>(x)));
            return new ResponseT<List<HotelDTO>>(true, h, null);

        }

        public async Task<ResponseT<HotelDTO>> GetByIdAsync(int id, int UserId)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId && x.Status == Enums.Status.Active);
            var hotel = await _context.Hotels.Include(x => x.rooms.Where(z=>z.status== Status.Active)).ThenInclude(r => r.Images.Where(z => z.status == Status.Active)).Where(z => z.status == Status.Active).FirstOrDefaultAsync(x => (x.Id == id));

            if (hotel == null || (user != null && user.Role == Enums.Role.Hoteladmin && hotel.CreatorId != user.Id))
            {
                return new ResponseT<HotelDTO>(false, null, "hotel not found");
            }

            if(user != null && user.Role != Enums.Role.SuperAdmin && hotel.status != Enums.Status.Active)
            {
                return new ResponseT<HotelDTO>(false, null, "hotel not found");
            }
            if(user == null && hotel.status != Enums.Status.Active)
            {
                return new ResponseT<HotelDTO>(false, null, "hotel not found");
            }
            return new ResponseT<HotelDTO>(true, _mapper.Map<HotelDTO>(hotel), null);


        }
        public async Task<ResponseC> DeleteHotelAcync(int hotelId, int adminId)
        {
            var hotel = await _context.Hotels.Include(x => x.rooms).FirstAsync(x => x.Id == hotelId);

            if (hotel == null || hotel.CreatorId != adminId ||
              hotel.status != Enums.Status.Active) return new ResponseC(false, "hotel not found");

            foreach (var item in hotel.rooms)
            {
                if (item.avaliable) return new ResponseC(false, "Cannot delete hotel with existing rooms");

            }
            hotel.status = Enums.Status.Deleted;
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "hotel deleted successfully");

        }
        public async Task<ResponseC> UpdateHotelAsync(UpdateHotelDTO dto, int adminId)
        {

            var hotel = await _context.Hotels.FirstOrDefaultAsync(x => (x.Id == dto.Id) && (x.CreatorId == adminId) && x.status == Enums.Status.Active);
            if (hotel == null) return new ResponseC(false, "hotel does not exists");
            hotel.name = dto.name;
            hotel.address = dto.address;
            hotel.city = dto.city;
            hotel.featuredImage = dto.featuredImage;
            hotel.ModifierId = adminId;
            hotel.LastModifiedDate = DateTime.Now;
            hotel.CreatorId = adminId;
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "Hotel Updated successfully");

        }


    }
}
