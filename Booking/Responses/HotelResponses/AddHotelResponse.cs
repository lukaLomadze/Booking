using Booking.Models.Entities;

namespace Booking.Responses.HotelResponses
{
    public class AddHotelResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AddHotelResponse(bool s,  string m)
        {
            Success = s;
      
            Message = m;
        }
    }
}
