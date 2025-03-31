using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses;
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
        public async Task<ResponseC> Register(UserRegisterDTO userRegisterDTO)
        {
            return await _authService.Register(userRegisterDTO);
        }


        [HttpPost("Login")]
        public async Task<ResponseT<string>> Login(UserLoginDTO userLoginDTO)
        {
            return await _authService.Login(userLoginDTO);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ResponseT<List<User>>> GetAll()
        {
            return await _authService.GetAll();
        }
    }
}
