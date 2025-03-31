using AutoMapper;
using Booking.Data;
using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Reservation;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Booking.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSender;
        public ReservationService(ApplicationDbContext context, IMapper mapper, IEmailSenderService emailSender)
        {
            _mapper = mapper;
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<ResponseC> AddAsync(AddReservationDTO dto, int userId)
        {
            var room = await _context.Rooms.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.Id == dto.roomId && x.status == Status.Active);
            if (room == null) return new ResponseC(false, "Room not found");
            if (dto.checkInData >= dto.checkOutData) return new ResponseC(false, "dates are incorrect");
            if (!room.avaliable || !isfree(room, dto.checkInData, dto.checkOutData)) return new ResponseC(false, "Room is not avaliable");
            var user = await _context.Users.FindAsync(userId);
            Random random = new Random();
            string c = "";
            for (int i = 0; i < 8; i++)
            {
                c += random.Next(0, 10);
            }


            var rese = new Reservation()
            {
                roomId = dto.roomId,
                checkInData = dto.checkInData,
                checkOutData = dto.checkOutData,
                isConfirmed = false,
                userId = userId,
                totalPrice = (dto.checkOutData - dto.checkInData).Days * room.pricePerNight,
                CreatorId = userId,
                Confirmation = c


            };
            await _context.Reservations.AddAsync(rese);
            await _context.SaveChangesAsync();
            await _emailSender.SendEmail(user.email, "Reservation", $"Your Confirmation code : {c}");

            return new ResponseC(true, "room booked");



        }

        public async Task<ResponseC> DeleteAsync(int id, int userId)
        {

            var rese = await _context.Reservations.FindAsync(id);
            if (rese == null || rese.CreatorId != userId || rese.status != Enums.Status.Active) return new ResponseC(false, "Reservation not found");
            rese.status = Enums.Status.Deleted;
            _context.Reservations.Update(rese);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "reservation Deleted");


        }

        public async Task<ResponseC> UpdateAsync(UpdateReservationDTO dto, int userId)
        {


            var rese = await _context.Reservations.FirstOrDefaultAsync(x =>
            x.Id == dto.Id && x.CreatorId == userId && x.status == Status.Active);
            if (rese == null) return new ResponseC(false, "ReservationNotFound");
            if (dto.checkInData >= dto.checkOutData) return new ResponseC(false, "dates are incorrect");
            rese.totalPrice = rese.totalPrice / (rese.checkOutData - rese.checkInData).Days * (dto.checkOutData - dto.checkInData).Days;
            rese.checkOutData = dto.checkOutData;
            rese.checkInData = dto.checkInData;
            _context.Reservations.Update(rese);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "Reservation updated");






        }




        public async Task<ResponseT<List<ReservationDTO>>> GetAllAsync(string role, int userId)
        {

            if (role == null) return new ResponseT<List<ReservationDTO>>(false, null, "Error");
            List<Reservation> reses = null;
            var r = new List<ReservationDTO>();
            if (role == Role.SuperAdmin.ToString()) reses = await _context.Reservations.ToListAsync();
            if (role == Role.Hoteladmin.ToString()) reses = await _context.Reservations.Include(x => x.room)
                    .Where(x => x.room.CreatorId == userId && x.status == Status.Active).ToListAsync();
            if (role == Role.Guest.ToString()) reses = await _context.Reservations.Include(x => x.room)
                    .Where(x => x.CreatorId == userId && x.status == Status.Active).ToListAsync();

            foreach (var rese in reses)
            {
                r.Add(_mapper.Map<ReservationDTO>(rese));
            }

            return new ResponseT<List<ReservationDTO>>(true, r, null);








        }
        public async Task<ResponseT<ReservationDTO>> GetByIdAsync(int id, string role, int userId)
        {
            if (role == null) return new ResponseT<ReservationDTO>(false, null, "Error");
            Reservation reses = null;

            if (role == Role.SuperAdmin.ToString()) reses = await _context.Reservations.FindAsync(id);

            if (role == Role.Hoteladmin.ToString()) reses = await _context.Reservations.Include(x => x.room)
                    .FirstOrDefaultAsync(x => x.room.CreatorId == userId && x.Id == id && x.status == Status.Active);

            if (role == Role.Guest.ToString()) reses = await _context.Reservations.Include(x => x.room)
                    .FirstOrDefaultAsync(x => x.CreatorId == userId && x.Id == id && x.status == Status.Active);


            return new ResponseT<ReservationDTO>(true, _mapper.Map<ReservationDTO>(reses), null);



        }



        public async Task<ResponseC> ConfirmReservationAsync(ConfirmReservationDTO dto, int userId)
        {



            var rese = await _context.Reservations
                    .FirstOrDefaultAsync(x => x.CreatorId == userId && x.Id == dto.Id && x.status == Status.Active);
            if (rese == null) return new ResponseC(false, "Reservation not found");
            if (rese.Confirmation == dto.Confirmation)
            {
                rese.isConfirmed = true;
            }
            else return new ResponseC(false, "ConfirmationText is incorrect");
            _context.Reservations.Update(rese);
            await _context.SaveChangesAsync();
            return new ResponseC(true, "Reservation confirmed");


        }

        public async Task<ResponseT<List<ReservationDTO>>> GetRoomsAsync(int roomId, string role, int userId)
        {

            var res = await GetAllAsync(role, userId);
            var r = new List<ReservationDTO>();
            foreach (var item in res.Data)
            {
                if (item.roomId == roomId) r.Add(item);

            }
            return new ResponseT<List<ReservationDTO>>(true, r, null);

        }





        #region
        private bool isfree(Room room, DateTime checkIn, DateTime checkOut)
        {
            if (room == null) return false;
            if (room.Reservations == null) return true;
            foreach (var res in room.Reservations)
            {
                if (!(res.checkInData >= checkOut || res.checkOutData <= checkIn))
                {
                    return false;
                }
            }
            return true;
        }





        #endregion


    }


}

