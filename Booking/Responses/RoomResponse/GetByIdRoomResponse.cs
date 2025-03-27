using Booking.Models.Entities;

namespace Booking.Responses.RoomResponse
{
    public class GetByIdRoomResponse
    {
        public bool Success { get; set; }
        public Room Rooms { get; set; }
        public string Message { get; set; }
        public GetByIdRoomResponse(bool s, Room r, string m)
        {
            Success = s;
            Rooms = r;
            Message = m;
        }
    }
}
