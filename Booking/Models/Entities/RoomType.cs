using Booking.Enums;

namespace Booking.Models.Entities
{
    public class RoomType :BaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status status { get; set; } = Status.Active;
    }
}
