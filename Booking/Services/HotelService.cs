using Booking.Data;
using Booking.Enums;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses.HotelResponses;
using Microsoft.EntityFrameworkCore;
namespace Booking.Services
{
    public class HotelService : IHotelService
    {

        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context) { 
            _context = context;
        
        }

        public async Task<AddHotelResponse> CreateHotelAsync(AddHotelDTO dto)
        {
            try
            {
                await _context.Hotels.AddAsync(new Hotel()
                {
                    name = dto.name,
                    address = dto.address,
                    city = dto.city,
                    featuredImage = dto.featuredImage,
                    CreatorId = dto.CreatorId});
                await _context.SaveChangesAsync();
                return new AddHotelResponse(true, "hotel added successfully");

            }catch (Exception ex)
            {
                return new AddHotelResponse(false, ex.Message);
            }


        } 
        public async Task<GetAllHotelResponse> GetAllAsync()
        {
            try { 
   
                var hotels =  await _context.Hotels.Include(x => x.rooms).ToListAsync();
                return new GetAllHotelResponse(true, hotels, null);

            }catch (Exception ex)
            {
                return new GetAllHotelResponse(false, null, ex.Message);


            }

            

        }
        public async Task<GetByIdHotelResponse> GetByIdAsync(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
                if (hotel == null) return new GetByIdHotelResponse(false, null,"hotel does not exists");
                
                return new GetByIdHotelResponse(true, hotel,null);
            }
            catch (Exception ex)
            {
                return new GetByIdHotelResponse(false,null, ex.Message);
            }

        }
        public async Task<DeleteHotelResponse> DeleteHotelAcync(int hotelId, int adminId)
        {
            try {
                var hotel = _context.Hotels.FirstOrDefault(x => (x.Id == hotelId ) && ( x.CreatorId == adminId));
                if (hotel == null) return new DeleteHotelResponse(false, "hotel does not exists");
                 _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return new DeleteHotelResponse(true, "hotel deleted successfully");
            }
            catch (Exception ex) {
                return new DeleteHotelResponse(false, ex.Message);


            }
        }
        public async Task<UpdateHotelResponse> UpdateHotelAsync(UpdateHotelDTO dto, int adminId)
        {
            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x =>( x.Id == dto.Id) && (x.CreatorId == adminId));
                if (hotel == null ) return new UpdateHotelResponse(false,  "hotel does not exists");

                hotel.city = dto.city;
                hotel.address = dto.address;
                hotel.name = dto.name;
                hotel.featuredImage = dto.featuredImage;
                hotel.ModifierId = adminId;
                hotel.LastModifiedDate= DateTime.Now;

                await _context.SaveChangesAsync();


                return new UpdateHotelResponse(true,  "Hotel Updated successfully");
            }
            catch (Exception ex)
            {
                return new UpdateHotelResponse(false, ex.Message);
            }



        }

       public async Task<GetAllHotelResponse>  GetUsersAllAsync(int adminId)
        {
            try
            {

                var hotels =  await _context.Hotels.Where(x => x.CreatorId == adminId).ToListAsync();
                return new GetAllHotelResponse(true, hotels, null);

            }
            catch (Exception ex)
            {
                return new GetAllHotelResponse(false, null, ex.Message);


            }


        }

        public  async Task<GetByIdHotelResponse> GetUsersByIdAsync(int id, int adminId)
        {
        try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => (x.Id == id) && (x.CreatorId == adminId));
                if (hotel == null) return new GetByIdHotelResponse(false, null,"hotel does not exists");
                
                return new GetByIdHotelResponse(true, hotel,null);
}
            catch (Exception ex)
            {
                return new GetByIdHotelResponse(false, null, ex.Message);
            }
         
        }
    }
}
