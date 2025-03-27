using Booking.Enums;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses.HotelResponses;

namespace Booking.Interfaces
{
    public interface IHotelService
    {
        public Task<AddHotelResponse> CreateHotelAsync(AddHotelDTO dto);
        public  Task<GetAllHotelResponse> GetAllAsync();
        public Task<GetByIdHotelResponse> GetByIdAsync(int id);
        public  Task<DeleteHotelResponse> DeleteHotelAcync(int hotelId, int userId);
        public Task<UpdateHotelResponse> UpdateHotelAsync(UpdateHotelDTO dto, int adminId);

        public Task<GetAllHotelResponse> GetUsersAllAsync(int adminId);
        public Task<GetByIdHotelResponse> GetUsersByIdAsync(int id, int adminId);


    }
}
