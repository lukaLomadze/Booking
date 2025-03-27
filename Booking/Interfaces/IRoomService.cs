using Booking.Models.DTOs;
using Booking.Responses.RoomResponse;

namespace Booking.Interfaces
{
    public interface IRoomService
    {
        public Task<AddRoomResponse> AddAsync(AddRoomDTO dto, int rAdminId);
        public Task<GetAllRoomResponse> GetAllAsync();
        public Task<GetByIdRoomResponse> GetByIdAsync(int id);

        public Task<GetAllRoomResponse> GetAdminsAllAsync(int AdminId, int rAdminId);
        public Task<GetByIdRoomResponse> GetAdminsByIdAsync(int id, int AdminId, int rAdminId);

        public Task<DeleteRoomResponse> DeleteAsync(int id, int adminId);
        public Task<UpdateRoomResponse> UpdateAsync(UpdateRoomDTO dto, int adminId);

        public Task<GetRoomTypesResponse> GetRoomTypes();





    }
}
