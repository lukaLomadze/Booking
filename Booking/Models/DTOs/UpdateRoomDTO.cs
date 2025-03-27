namespace Booking.Models.DTOs
{
    public class UpdateRoomDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal pricePerNight { get; set; }
        public bool avaliable { get; set; }
        public int maximumGuests { get; set; }
        public int roomTypeId { get; set; }
    }
}
