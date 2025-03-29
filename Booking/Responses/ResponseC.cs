namespace Booking.Responses
{
    public class ResponseC
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseC(bool s,  string m)
        {
            Success = s;
            Message = m;
        }
    }
}
