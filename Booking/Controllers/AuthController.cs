using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Responses.AuthResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<RegisterResponse> Register(UserRegisterDTO userRegisterDTO)
        {
           return  await _authService.Register(userRegisterDTO);
        }


        [HttpPost("Login")]
        public async Task<LoginResponse> Login(UserLoginDTO userLoginDTO)
        {
            return  await _authService.Login(userLoginDTO);
        }
       


    }
}
