using AutoMapper;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Room;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
    public class RoomService : IRoomService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseC> AddAsync(AddRoomDTO dto, int adminId)
        {
            try
            {
                var rhotel = _context.Hotels.FirstOrDefault(x => (x.CreatorId == adminId) && (x.Id == dto.hotelId) && x.status == Enums.Status.Active);
                if (rhotel == null) return new ResponseC(false, "hotel's Id is incorrect");
                var rType = _context.RoomTypes.FirstOrDefault(x => x.Id == dto.roomTypeId);
                if (rType == null) return new ResponseC(false, "Room type id is incorrect");

                var room = _mapper.Map<Room>(dto);
                room.CreatorId = adminId;
                room.roomType = rType;
                room.hotel = rhotel;
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();
                return new ResponseC(true, "Room added successfully");
            }
            catch (Exception ex)
            {
                return new ResponseC(false, ex.Message);
            }
        }

        public async Task<ResponseC> DeleteAsync(int id, int adminId)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(id);
                if (room == null || room.CreatorId != adminId) return new ResponseC(false, "Room not found");
                room.status = Enums.Status.Deleted;
                room.avaliable = false;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return new ResponseC(true, "Room deleted");
            }
            catch (Exception ex)
            {
                return new ResponseC(false, ex.Message);

            }

        }

        public async Task<ResponseT<List<RoomDTO>>> GetAllAsync(int userId)
        {

            try
            {
                var rooms = await _context.Rooms.Include(x => x.Images).Include(h => h.hotel)
                    .Include(x => x.roomType).Where(x => x.status == Enums.Status.Active).ToListAsync();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status == Enums.Status.Active);
                if (user != null && rooms != null && user.Role == Enums.Role.Hoteladmin)
                {
                    rooms = rooms.Where(x => x.CreatorId == userId).ToList();
                }
                var r = new List<RoomDTO>();
                if (rooms != null)
                {
                    foreach (var item in rooms)
                    {
                        var room = _mapper.Map<RoomDTO>(item);
                        
                        room.hotel.rooms = null;
                        r.Add(room);
                    }


                }


                return new ResponseT<List<RoomDTO>>(true, r, null);
            }
            catch (Exception ex)
            {
                return new ResponseT<List<RoomDTO>>(false, null, ex.Message);
            }

        }

        public async Task<ResponseT<RoomDTO>> GetByIdAsync(int id, int userId)
        {
            try
            {
                var room = await _context.Rooms.Include(x => x.Images).Include(h => h.hotel).Include(x => x.Reservations)
                    .Include(x => x.roomType).FirstOrDefaultAsync(r => r.Id == id && r.status == Enums.Status.Active);
                var user = await _context.Users.FirstOrDefaultAsync(x => (x.Id == userId) && (x.Status == Enums.Status.Active));

                if (room == null || (user != null && user.Role == Enums.Role.Hoteladmin && room.CreatorId != userId))
                {
                    return new ResponseT<RoomDTO>(false, null, "Room not found");
                }
                return new ResponseT<RoomDTO>(true, _mapper.Map<RoomDTO>(room), null);
            }
            catch (Exception ex)
            {
                return new ResponseT<RoomDTO>(false, null, ex.Message);
            }

        }

        public async Task<ResponseC> UpdateAsync(UpdateRoomDTO dto, int adminId)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(x => (x.Id == dto.id) && (x.CreatorId == adminId) && x.status == Enums.Status.Active);
                if (room == null) return new ResponseC(false, "Room not found");
                room.name = dto.name;
                room.pricePerNight = dto.pricePerNight;
                room.avaliable = dto.avaliable;
                room.maximumGuests = dto.maximumGuests;
                room.roomTypeId = dto.roomTypeId;
                room.status = dto.status;
                room.LastModifiedDate = DateTime.Now;
                room.ModifierId = adminId;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return new ResponseC(true, "room Updated");

            }
            catch (Exception ex)
            {
                return new ResponseC(true, ex.ToString());
            }
        }


        public async Task<ResponseT<List<RoomType>>> GetRoomTypes()
        {
            try
            {
                var rooms = await _context.RoomTypes.Where(x => x.status == Enums.Status.Active).ToListAsync();
                return new ResponseT<List<RoomType>>(true, rooms, null);
            }
            catch (Exception ex)
            {
                return new ResponseT<List<RoomType>>(false, null, ex.Message);
            }

        }
    }
}
