﻿using AutoMapper;
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
        private readonly IReservationService _reservationService;

        public RoomService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseC> AddAsync(AddRoomDTO dto, int adminId)
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

        public async Task<ResponseC> DeleteAsync(int id, int adminId)
        {
            var room = await _context.Rooms.Include(x => x.Reservations).Include(x=> x.Images).FirstOrDefaultAsync(x => x.Id == id && x.CreatorId == adminId && x.status == Enums.Status.Active);//.FindAsync(id);
            if (room == null) return new ResponseC(false, "Room not found");
            foreach (var item in room.Reservations)
            {
                if (item.status == Enums.Status.Active) return new ResponseC(false, "Cannot delete room with exising reservations");
            }
            foreach (var item in room.Images)
            {
                item.status = Enums.Status.Deleted;
            }

            room.status = Enums.Status.Deleted;
            room.avaliable = false;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "Room deleted");


        }

        public async Task<ResponseT<List<RoomDTO>>> GetAllAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status == Enums.Status.Active);
            var rooms = await _context.Rooms.Include(x => x.Images.Where(z=>z.status == Enums.Status.Active)).Include(h => h.hotel)
                .Include(x => x.roomType).ToListAsync();
            if (user != null && rooms != null)
            {

                if (user.Role == Enums.Role.Hoteladmin) rooms = rooms.Where(x => x.CreatorId == userId).ToList();
            }
            if ((user == null || user.Role != Enums.Role.SuperAdmin) && rooms != null) rooms = rooms.Where(r => r.status == Enums.Status.Active).ToList();
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

        public async Task<ResponseT<RoomDTO>> GetByIdAsync(int id, int userId)
        {

            var room = await _context.Rooms.Include(x => x.Images.Where(z => z.status == Enums.Status.Active)).Include(h => h.hotel)
                .Include(x => x.roomType).FirstOrDefaultAsync(r => r.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(x => (x.Id == userId) && (x.Status == Enums.Status.Active));

            if (room == null || (user != null && user.Role == Enums.Role.Hoteladmin && room.CreatorId != userId))
            {
                return new ResponseT<RoomDTO>(false, null, "Room not found");
            }

            if ((user == null || user.Role != Enums.Role.SuperAdmin) && room.status != Enums.Status.Active)
            {
                return new ResponseT<RoomDTO>(false, null, "Room not found");
            }

            return new ResponseT<RoomDTO>(true, _mapper.Map<RoomDTO>(room), null);


        }

        public async Task<ResponseC> UpdateAsync(UpdateRoomDTO dto, int adminId)
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

        public async Task<ResponseT<List<RoomDTO>>> FilterAsync(FilterRoomDTO filter, int userId)
        {


            var respons = new List<RoomDTO>();


            var rooms = (await GetAllAsync(userId)).Data.AsQueryable();

            if (filter.RoomTypeId.HasValue)
                rooms = rooms.Where(r => r.roomType.Id == filter.RoomTypeId.Value);

            if (filter.PriceFrom.HasValue)
                rooms = rooms.Where(r => r.pricePerNight >= filter.PriceFrom.Value);

            if (filter.PriceTo.HasValue)
                rooms = rooms.Where(r => r.pricePerNight <= filter.PriceTo.Value);

            if (filter.MaximumGuests.HasValue)
                rooms = rooms.Where(r => r.maximumGuests >= filter.MaximumGuests.Value);

            if (filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            {

                var checkIn = filter.CheckIn.Value;
                var checkOut = filter.CheckOut.Value;

                foreach (var item in rooms)
                {
                    var rese = await _context.Reservations.Where(x => x.roomId == item.Id && x.status == Enums.Status.Active
                    && (x.checkOutData <= checkIn || x.checkInData >= checkOut)).ToListAsync();
                    if (rese.Count() == 0) respons.Add(item);
                }


            }
            return new ResponseT<List<RoomDTO>>(true, rooms.ToList(), null);


        }


        public async Task<ResponseT<List<RoomType>>> GetRoomTypes()
        {
            var rooms = await _context.RoomTypes.Where(x => x.status == Enums.Status.Active).ToListAsync();
            return new ResponseT<List<RoomType>>(true, rooms, null);
        }
    }
}
