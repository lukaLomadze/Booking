using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Reservation;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System.Security.Claims;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpPost]
        [Authorize(Roles = "Guest")]
        public async  Task<ResponseC> AddAsync(AddReservationDTO dto)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value , out userId);
            return await reservationService.AddAsync(dto, userId);
        }
        [HttpPut]
        [Authorize(Roles = "Guest")]
        public async Task<ResponseC> UpdateAsync(UpdateReservationDTO dto)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await reservationService.UpdateAsync(dto ,userId);

        }
        [HttpDelete]
        [Authorize(Roles = "Guest")]
        public async Task<ResponseC> DeleteAsync(int id)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await reservationService.DeleteAsync(id, userId);
        }


        [HttpGet]
        [Authorize]
        public async Task<ResponseT<List<ReservationDTO>>> GetAllAsync()
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return await reservationService.GetAllAsync(role, userId);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ResponseT<ReservationDTO>> GetByIdAsync(int id)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return await reservationService.GetByIdAsync(id, role,userId);
        }

        [HttpGet("Room's Reservations {id}")] 
        [Authorize]
        public Task<ResponseT<List<ReservationDTO>>> GetRoomsAsync(int roomId)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return reservationService.GetRoomsAsync(roomId,role, userId);
        }

        [HttpPost("Confirm")]
        [Authorize(Roles = "Guest")]
        public Task<ResponseC> ConfirmReservationAsync(ConfirmReservationDTO dto)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
          
            return reservationService.ConfirmReservationAsync(dto,userId);


        }






    }
}
