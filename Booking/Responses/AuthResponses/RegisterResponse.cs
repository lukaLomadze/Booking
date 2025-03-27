namespace Booking.Responses.AuthResponses
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public RegisterResponse(String m, bool s)
        {
            Success = s;
            Message = m;
        }
    }
}
