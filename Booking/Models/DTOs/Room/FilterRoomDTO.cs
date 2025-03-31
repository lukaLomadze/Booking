namespace Booking.Models.DTOs.Room
{
    public class FilterRoomDTO
    {
        public int? RoomTypeId { get; set; }
        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }
        public int? MaximumGuests { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
         
 
    }
}
