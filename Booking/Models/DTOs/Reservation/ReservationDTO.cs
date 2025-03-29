using Booking.Enums;
using Booking.Models.DTOs.Room;
using Booking.Models.Entities;

namespace Booking.Models.DTOs.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int roomId { get; set; }
        public DateTime checkInData { get; set; }
        public DateTime checkOutData { get; set; }
        public decimal totalPrice { get; set; }
        public bool isConfirmed { get; set; }
        public int userId { get; set; }
       
     
    }
}
