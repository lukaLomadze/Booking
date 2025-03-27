namespace Booking.Responses.RoomResponse
{
    public class DeleteRoomResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DeleteRoomResponse(bool s, string m)
        {
            Success = s;
            Message = m;
        }
    }
}
