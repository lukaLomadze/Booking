namespace Booking.Responses.HotelResponses
{
    public class UpdateHotelResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UpdateHotelResponse(bool s, string m)
        {
            Success = s;

            Message = m;
        }
    }
}
