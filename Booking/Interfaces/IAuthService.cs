
using Booking.Models.DTOs;
using Booking.Responses;


namespace Booking.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseC> Register(UserRegisterDTO registerDTO);
        Task<ResponseT<string>> Login(UserLoginDTO loginDTO);
    }
}
