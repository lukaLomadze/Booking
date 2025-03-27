namespace Booking.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Response(bool s,  string m)
        {
            Success = s;
            Message = m;
        }
    }
}
