using Booking.Data;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> AddAsync(AddReservationDTO dto)
        {
            try
            {
                var room = await _context.Rooms.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.Id == dto.roomId);
                if (room == null) return new Response(false, "Room not found");
                if (!room.avaliable || !isfree(room, dto.checkInData, dto.checkOutData)) return new Response(false, "Room is not avaliable");
                var rese = new Reservation()
                {
                    roomId = dto.roomId,
                    checkInData = dto.checkInData,
                    checkOutData = dto.checkOutData,
                    isConfirmed = dto.isConfirmed,
                    userId = dto.userId,
                    totalPrice = (dto.checkInData - dto.checkOutData).Days * room.pricePerNight,
                    CreatorId = dto.userId
                };
                await _context.Reservations.AddAsync(rese);
                await _context.SaveChangesAsync();
                return new Response(true, "room booked");
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message);

            }


        }

        public Task<Response> DeleteAsync(int id, int adminId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(UpdateReservationDTO dto)
        {
            throw new NotImplementedException();
        }







        private bool isfree(Room room, DateTime checkIn, DateTime checkOut)
        {
            if (room == null) return false;
            if (room.Reservations == null) return true;
            foreach (var res in room.Reservations)
            {
                if (!(res.checkInData > checkOut || res.checkOutData < checkIn))
                {
                    return false;
                }

            }
            return true;


        }


    }


}

