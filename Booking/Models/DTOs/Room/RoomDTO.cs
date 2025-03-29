using Booking.Enums;
using Booking.Models.Entities;
using Booking.Models.DTOs.Hotel;
using Booking.Models.DTOs.Reservation;

namespace Booking.Models.DTOs.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public HotelDTO hotel { get; set; }
        public decimal pricePerNight { get; set; }
        public bool avaliable { get; set; }
        public int maximumGuests { get; set; }
        public RoomTypeDTO roomType { get; set; }

        public List<ImageDTO> Images { get; set; } = new List<ImageDTO>();

      
    }
}
