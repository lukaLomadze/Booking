using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Hotel;
using Booking.Models.Entities;
using Booking.Responses;
using Booking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;
        public HotelController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpPost]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> AddHotel(AddHotelDTO dto)
        {
            int adminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out adminId);
            return await hotelService.CreateHotelAsync(dto, adminId);
        }

        [HttpGet]
        public async Task<ResponseT<List<HotelDTO>>> GetAll()
        {
            int UserId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out UserId); 
            return await hotelService.GetAllAsync(UserId);
        }


        [HttpGet("{id}")]
        public async Task<ResponseT<HotelDTO>> GetById(int id)
        {
            int UserId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out UserId);
            return await hotelService.GetByIdAsync(id, UserId);
        }


        [HttpDelete]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> DeleteHotel(int hotelId)
        {
            int adminId;
            int.TryParse(( User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out  adminId);
            return await hotelService.DeleteHotelAcync(hotelId, adminId);
        }


        [HttpPut]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> UpdateHotel(UpdateHotelDTO dto)
        {
            int adminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out adminId);
            return await hotelService.UpdateHotelAsync(dto, adminId);
        }





    }
}
