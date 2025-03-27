using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Responses.RoomResponse;
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
        public async Task<AddRoomResponse> AddAsync(AddRoomDTO dto)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.AddAsync(dto, rAdminId);

        }

        [HttpGet]
        public async Task<GetAllRoomResponse> GetAllAsync()
        {
            return await  roomService.GetAllAsync();
        }


        [HttpGet("{id}")]
        public async Task<GetByIdRoomResponse> GetByIdAsync(int id)
        {
            return await roomService.GetByIdAsync(id);
        }


        [HttpGet("Admin's all {id}")]
        [Authorize(Roles ="Hoteladmin")]
        public async Task<GetAllRoomResponse> GetAdminsAllAsync(int AdminId)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.GetAdminsAllAsync(AdminId, rAdminId);
        }


        [HttpGet("Admin's one {id} {AdminId}")]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<GetByIdRoomResponse> GetAdminsByIdAsync(int id, int AdminId)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.GetAdminsByIdAsync(id, AdminId , rAdminId);
        }


        [HttpDelete]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<DeleteRoomResponse> DeleteAsync(int id)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.DeleteAsync(id, rAdminId);
        }



        [HttpPut]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<UpdateRoomResponse> UpdateAsync(UpdateRoomDTO dto)
        {
            int rAdminId;
            int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out rAdminId);
            return await roomService.UpdateAsync(dto,rAdminId);
        }

        [HttpGet("GetRoomTypes")]
        public async Task<GetRoomTypesResponse> GetRoomTypes()
        {
            return await roomService.GetRoomTypes();
        }





    }
}
