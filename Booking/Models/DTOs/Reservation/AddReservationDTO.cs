using Booking.Models.Entities;

namespace Booking.Models.DTOs
{
    public class AddReservationDTO
    {
        public int roomId { get; set; }
        public DateTime checkInData { get; set; }
        public DateTime checkOutData { get; set; }
     
        
    }
}
