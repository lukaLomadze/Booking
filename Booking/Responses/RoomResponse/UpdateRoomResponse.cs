namespace Booking.Responses.RoomResponse
{
    public class UpdateRoomResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UpdateRoomResponse(bool s, string m)
        {
            Success = s;
            Message = m;
        }
    }
}
