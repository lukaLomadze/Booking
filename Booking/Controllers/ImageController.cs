using Booking.Interfaces;
using Booking.Models.DTOs.Image;
using Booking.Models;
using Booking.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }



        [HttpPost]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> AddAsync(AddImageDTO dto)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await _imageService.AddAsync(dto, userId);
        }
        [HttpGet]
        public async Task<ResponseT<List<ImageDTO>>> GetAllAsync()
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await _imageService.GetAllAsync(userId);
        }
        [HttpGet("{id}")]
        public async Task<ResponseT<ImageDTO>> GetByIdAsync(int id)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await _imageService.GetByIdAsync(id, userId);
        }
        [HttpDelete]
        [Authorize(Roles = "Hoteladmin")]
        public async Task<ResponseC> DeleteAsync(int id)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await _imageService.DeleteAsync(id, userId);

        }







    }
}
