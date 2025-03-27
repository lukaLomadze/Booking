using Booking.Models.Entities;

namespace Booking.Responses.HotelResponses
{
    public class GetByIdHotelResponse
    {
        public bool Success { get; set; }
        public Hotel Hotels { get; set; }
        public string Message { get; set; }
        public GetByIdHotelResponse(bool s, Hotel h, string m)
        {
            Success = s;
            Hotels = h;
            Message = m;
        }

        }
    }
