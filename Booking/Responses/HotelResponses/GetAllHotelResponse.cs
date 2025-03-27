using Booking.Models.Entities;

namespace Booking.Responses.HotelResponses
{
    public class GetAllHotelResponse
    {
        public bool Success { get; set; }
        public List<Hotel> Hotels  { get; set; }
        public string Message  { get; set; }
        public GetAllHotelResponse(bool s, List<Hotel> h, string m)
        {
            Success = s;
            Hotels = h;  
            Message = m;
        }
    }
}
