using Booking.Enums;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Reservation;
using Booking.Models.Entities;
using Booking.Responses;

namespace Booking.Interfaces
{
    public interface IReservationService
    {
        public Task<ResponseC> AddAsync(AddReservationDTO dto, int userId);
      
        public Task<ResponseC> UpdateAsync(UpdateReservationDTO dto, int userId);
        public Task<ResponseC> DeleteAsync(int id , int userId);
        public Task<ResponseT<List<ReservationDTO>>> GetAllAsync(string role, int userId);
        public Task<ResponseT<ReservationDTO>> GetByIdAsync(int id, string role, int userId);
        public Task<ResponseT<List<ReservationDTO>>> GetRoomsAsync(int roomId ,string role, int userId);

        public Task<ResponseC> ConfirmReservationAsync(ConfirmReservationDTO dto, int userId);





    }
}
