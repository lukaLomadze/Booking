using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async  Task<Response> AddAsync(AddReservationDTO dto)
        {
            return await reservationService.AddAsync(dto);
        }
        [HttpPut]
        [Authorize(Roles = "Guest")]
        public async Task<Response> UpdateAsync(UpdateReservationDTO dto)
        {
            return await reservationService.UpdateAsync(dto);

        }
        [HttpDelete]
        [Authorize(Roles = "Guest")]
        public async Task<Response> DeleteAsync(int id, int adminId)
        {
            return await reservationService.DeleteAsync(id, adminId);
        }
    }
}
