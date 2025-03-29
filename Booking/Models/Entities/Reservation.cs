using Booking.Enums;

namespace Booking.Models.Entities
{
    public class Reservation : BaseClass
    {
        public int Id { get; set; }
        public int roomId { get; set; }
        public Room room { get; set; }
        public DateTime checkInData  { get; set; }
        public DateTime checkOutData { get; set; }
        public decimal totalPrice { get; set; }
        public bool isConfirmed { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public Status status { get; set; } = Status.Active;
        public string Confirmation { get; set; }

    }
}
