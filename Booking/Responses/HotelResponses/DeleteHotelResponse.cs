namespace Booking.Responses.HotelResponses
{
    public class DeleteHotelResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DeleteHotelResponse(bool s, string m)
        {
            Success = s;
         
            Message = m;
        }
    }
}
