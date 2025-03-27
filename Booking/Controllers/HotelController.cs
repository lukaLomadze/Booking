using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses.HotelResponses;
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
        public async Task<AddHotelResponse> AddHotel(AddHotelDTO dto)
        {
            return await hotelService.CreateHotelAsync(dto);
        }

        [HttpGet]
        public async Task<GetAllHotelResponse> GetAll()
        {
            return await hotelService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetByIdHotelResponse> GetById(int id)
        {
            return await hotelService.GetByIdAsync(id);
        }
        [HttpGet("Admin's all {adminId}")]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<GetAllHotelResponse> GetUsersAllHotel (int adminId)
        {
            int Id;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out Id);
            if (Id != adminId) return new GetAllHotelResponse(false, null, "Admin's ID is incorrect");
            return await hotelService.GetUsersAllAsync(adminId);
        }

        [HttpGet("Admin's one {id} {adminId}")]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<GetByIdHotelResponse> GetUsersHotelById(int id, int adminId)
        {
            int Id;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out Id);
            if (Id != adminId) return new GetByIdHotelResponse(false,null, "Admin's ID is incorrect");
            return await hotelService.GetUsersByIdAsync(id, adminId);
        }

        [HttpDelete]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<DeleteHotelResponse> DeleteHotel(int hotelId)
        {
            int adminId;
            int.TryParse(( User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out  adminId);
            return await hotelService.DeleteHotelAcync(hotelId, adminId);
        }

        [HttpPut]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<UpdateHotelResponse> UpdateHotel(UpdateHotelDTO dto)
        {
            int adminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out adminId);
            return await hotelService.UpdateHotelAsync(dto, adminId);
        }





    }
}
