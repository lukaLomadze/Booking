
using Booking.Models.DTOs;
using Booking.Responses.AuthResponses;


namespace Booking.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(UserRegisterDTO registerDTO);
        Task<LoginResponse> Login(UserLoginDTO loginDTO);
    }
}
