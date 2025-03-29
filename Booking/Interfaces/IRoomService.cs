using Azure;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Room;
using Booking.Models.Entities;
using Booking.Responses;


namespace Booking.Interfaces
{
    public interface IRoomService
    {
        public Task<ResponseC> AddAsync(AddRoomDTO dto, int adminId);
        public Task<ResponseT<List<RoomDTO>>> GetAllAsync(int userId);
        public Task<ResponseT<RoomDTO>> GetByIdAsync(int id, int userId);
        public Task<ResponseC> DeleteAsync(int id, int adminId);
        public Task<ResponseC> UpdateAsync(UpdateRoomDTO dto, int adminId);
        public Task<ResponseT<List<RoomType>>> GetRoomTypes();





    }
}
