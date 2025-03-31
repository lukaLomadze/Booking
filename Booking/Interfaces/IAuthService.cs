
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses;


namespace Booking.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseC> Register(UserRegisterDTO registerDTO);
        public Task<ResponseT<string>> Login(UserLoginDTO loginDTO);
        public Task<ResponseT<List<User>>> GetAll();
    }
}
