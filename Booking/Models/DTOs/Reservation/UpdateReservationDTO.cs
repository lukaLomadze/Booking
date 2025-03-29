using Booking.Models.Entities;

namespace Booking.Models.DTOs
{
    public class UpdateReservationDTO
    {
        public int Id { get; set; }
        public DateTime checkInData { get; set; }
        public DateTime checkOutData { get; set; }
        
        
    }
}
