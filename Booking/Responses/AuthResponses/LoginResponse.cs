namespace Booking.Responses.AuthResponses
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public string Token { get; set; }

        public LoginResponse(bool s, string m)
        {
            Message = m;
            Success = s;
            Token = String.Empty;

            
        }
    }
}
