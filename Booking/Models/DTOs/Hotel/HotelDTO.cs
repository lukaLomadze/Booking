using Booking.Models.DTOs.Room;
using Booking.Models.Entities;

namespace Booking.Models.DTOs.Hotel
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string featuredImage { get; set; }
        public List<RoomDTO> rooms { get; set; } = new List<RoomDTO>();

    }
}
