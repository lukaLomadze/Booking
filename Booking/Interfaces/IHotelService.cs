using Booking.Enums;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Hotel;
using Booking.Models.Entities;
using Booking.Responses;


namespace Booking.Interfaces
{
    public interface IHotelService
    {
        public Task<ResponseC> CreateHotelAsync(AddHotelDTO dto, int adminId);
        public  Task<ResponseT<List<HotelDTO>>> GetAllAsync(int UserId);
        public Task<ResponseT<HotelDTO>> GetByIdAsync(int id, int UserId);
        public  Task<ResponseC> DeleteHotelAcync(int hotelId, int userId);
        public Task<ResponseC> UpdateHotelAsync(UpdateHotelDTO dto, int adminId);

     


    }
}
