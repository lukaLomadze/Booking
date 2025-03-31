using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Room;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;

        }
        [HttpPost]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> AddAsync(AddRoomDTO dto)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.AddAsync(dto, rAdminId);

        }

        [HttpGet]
        public async Task<ResponseT<List<RoomDTO>>> GetAllAsync()
        {
            int userId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out userId);
            return await  roomService.GetAllAsync(userId);
        }


        [HttpGet("{id}")]
        public async Task<ResponseT<RoomDTO>> GetByIdAsync(int id)
        {
            int userId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out userId);
            return await roomService.GetByIdAsync(id, userId);
        }


       


        [HttpDelete]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> DeleteAsync(int id)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.DeleteAsync(id, rAdminId);
        }



        [HttpPut]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> UpdateAsync(UpdateRoomDTO dto)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.UpdateAsync(dto,rAdminId);
        }

        [HttpPost("Filter")]

        public async Task<ResponseT<List<RoomDTO>>> FilterAsync(FilterRoomDTO filter)
        {
            int userId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out userId);
            return await roomService.FilterAsync(filter, userId);
        }




        [HttpGet("GetRoomTypes")]
        public async Task<ResponseT<List<RoomType>>> GetRoomTypes()
        {
            return await roomService.GetRoomTypes();
        }





    }
}
