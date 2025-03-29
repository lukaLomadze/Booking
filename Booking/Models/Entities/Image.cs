using Booking.Enums;

namespace Booking.Models.Entities
{
    public class Image: BaseClass
    {
        public int Id { get; set; }
        public int roomId { get; set; }
        public Room room { get; set; }
        public string source { get; set; }
        public Status status { get; set; } = Status.Active;
    }
}
