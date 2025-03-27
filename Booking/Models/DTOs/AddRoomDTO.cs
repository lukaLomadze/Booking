namespace Booking.Models.DTOs
{
    public class AddRoomDTO
    {
        public string name { get; set; }
        public int hotelId { get; set; }
        public int adminId { get; set; }
        public decimal pricePerNight { get; set; }
        public bool avaliable { get; set; }
        public int maximumGuests { get; set; }
        public int roomTypeId { get; set; }


    }
}
