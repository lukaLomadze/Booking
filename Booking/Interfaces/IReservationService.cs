using Booking.Enums;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses;

namespace Booking.Interfaces
{
    public interface IReservationService
    {
        public Task<Response> AddAsync(AddReservationDTO dto);
      
        public Task<Response> UpdateAsync(UpdateReservationDTO dto);
        public Task<Response> DeleteAsync(int id , int adminId);
       // public Task<ResponseT<List<Reservation>>> GetAllAsync();
       // public Task<ResponseT<Reservation>> GetByIdAsync(int id, Role role, int userId);






    }
}
