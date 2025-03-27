using Booking.Data;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses.HotelResponses;
using Booking.Responses.RoomResponse;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
    public class RoomService : IRoomService
    {

        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<AddRoomResponse> AddAsync(AddRoomDTO dto, int rAdminId)
        {
            try
            {


                if (dto.adminId != rAdminId) return new AddRoomResponse(false, "Admin's Id is incorrect");

                var rhotel = _context.Hotels.FirstOrDefault(x => (x.CreatorId == rAdminId) && (x.Id == dto.hotelId));
                if (rhotel == null) return new AddRoomResponse(false, "hotel's Id is incorrect");
                var rType = _context.RoomTypes.FirstOrDefault(x => x.Id == dto.roomTypeId);
                if (rType == null) return new AddRoomResponse(false, "Room type id is incorrect");
                await _context.AddAsync(new Room()
                {
                    name = dto.name,
                    hotelId = dto.hotelId,
                    CreatorId = dto.adminId,
                    pricePerNight = dto.pricePerNight,
                    avaliable = dto.avaliable,
                    maximumGuests = dto.maximumGuests,
                    roomTypeId = dto.roomTypeId,
                    roomType = rType,
                    hotel = rhotel
                });
                await _context.SaveChangesAsync();
                return new AddRoomResponse(true, "Room added successfully");

            }
            catch (Exception ex)
            {
                return new AddRoomResponse(false, ex.Message);


            }
        }

        public async Task<DeleteRoomResponse> DeleteAsync(int id, int adminId)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefaultAsync(x => (x.Id == id) && (x.CreatorId == adminId));
                if (room == null) return new DeleteRoomResponse(false, "Room decannot be found");
                _context.Remove(room);
                await _context.SaveChangesAsync();
                return new DeleteRoomResponse(true, "Room deleted");
            }
            catch (Exception ex)
            {
                return new DeleteRoomResponse(false, ex.Message);

            }

        }

        public async Task<GetAllRoomResponse> GetAllAsync()
        {

            try
            {
                var rooms = await _context.Rooms.ToListAsync();
                return new GetAllRoomResponse(true, rooms, null);
            }
            catch (Exception ex)
            {
                return new GetAllRoomResponse(false, null, ex.Message);
            }

        }

        public async Task<GetAllRoomResponse> GetAdminsAllAsync(int AdminId, int rAdminId)
        {
            try
            {
                if(AdminId != rAdminId) return new GetAllRoomResponse(false, null, "Admin in is incorrect");
                var rooms = await _context.Rooms.Where(x => x.CreatorId == AdminId).ToListAsync();
                return new GetAllRoomResponse(true, rooms, null);
            }
            catch (Exception ex)
            {
                return new GetAllRoomResponse(false, null, ex.Message);
            }
        }

        public async Task<GetByIdRoomResponse> GetAdminsByIdAsync(int id, int AdminId, int rAdminId)
        {
            try
            {
                if (AdminId != rAdminId) return new GetByIdRoomResponse(false, null, "Admin in is incorrect");
                var room = await _context.Rooms.FirstOrDefaultAsync(r => (r.Id == id) && (r.CreatorId == AdminId));
                return new GetByIdRoomResponse(true, room, null);
            }
            catch (Exception ex)
            {
                return new GetByIdRoomResponse(false, null, ex.Message);
            }
        }

        public async Task<GetByIdRoomResponse> GetByIdAsync(int id)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
                return new GetByIdRoomResponse(true, room, null);
            }
            catch (Exception ex)
            {
                return new GetByIdRoomResponse(false, null, ex.Message);
            }

        }

        public async Task<UpdateRoomResponse> UpdateAsync(UpdateRoomDTO dto, int adminId)
        {

            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(x => (x.Id == dto.id) && (x.CreatorId == adminId));
                if (room == null) return new UpdateRoomResponse(false,  "Room not found");
                room.name = dto.name;
                room.pricePerNight = dto.pricePerNight;
                room.LastModifiedDate = DateTime.Now;
                room.avaliable = dto.avaliable;
                room.maximumGuests = dto.maximumGuests;
                room.roomTypeId = dto.roomTypeId;
                return new UpdateRoomResponse(true, "room Updated");

            }catch (Exception ex)
            {
                return new UpdateRoomResponse(true, ex.Message);


            }


        }


        public async Task<GetRoomTypesResponse> GetRoomTypes()
        {

            try
            {
                var rooms = await _context.RoomTypes.ToListAsync();
                return new GetRoomTypesResponse(true, rooms, null);
            }
            catch (Exception ex)
            {
                return new GetRoomTypesResponse(false, null, ex.Message);
            }

        }
    }
}
