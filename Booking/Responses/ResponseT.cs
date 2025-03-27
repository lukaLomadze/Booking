namespace Booking.Responses
{
    public class ResponseT<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseT(bool s, T d, string m)
        {
            Success = s;
            Message = m;
            Data = d;
        }

    }
}
