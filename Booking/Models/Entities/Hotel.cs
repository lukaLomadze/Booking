using Booking.Enums;

namespace Booking.Models.Entities
{
    public class Hotel : BaseClass
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string  featuredImage { get; set; }
        public List<Room> rooms { get; set; } = new List<Room>();
        public Status status { get; set; } = Status.Active;





    }
}
