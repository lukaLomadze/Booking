namespace Booking.Responses.RoomResponse
{
    public class AddRoomResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AddRoomResponse(bool s, string m)
        {
            Success = s;
            Message = m;
        }
    }
}
