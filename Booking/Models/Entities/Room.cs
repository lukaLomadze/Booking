namespace Booking.Models.Entities
{
    public class Room : BaseClass
    {
        public int Id { get; set; }
        public string name  { get; set; }
        public int hotelId { get; set; }
        public Hotel hotel { get; set; }
        public decimal pricePerNight { get; set; }
        public bool avaliable { get; set; }
        public int maximumGuests { get; set; }
        public int roomTypeId { get; set; }
        public RoomType roomType { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Image> Images { get; set; } = new List<Image>();




    }
}
