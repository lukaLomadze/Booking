using Booking.Models;
using Booking.Models.DTOs.Image;
using Booking.Responses;

namespace Booking.Interfaces
{
    public interface IImageService
    {
        public Task<ResponseC> AddAsync(AddImageDTO dto, int userId);
        public Task<ResponseT<List<ImageDTO>>> GetAllAsync(int userid);
        public Task<ResponseT<ImageDTO>> GetByIdAsync(int id, int userId);
        public Task<ResponseC> DeleteAsync(int  id, int userId);
    }
}
