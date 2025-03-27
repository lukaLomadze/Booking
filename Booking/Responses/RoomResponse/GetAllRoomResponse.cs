using Booking.Models.Entities;

namespace Booking.Responses.RoomResponse
{
    public class GetAllRoomResponse
    {
        public bool Success { get; set; }
        public List<Room> Rooms { get; set; }
        public string Message { get; set; }
        public GetAllRoomResponse(bool s, List<Room> r, string m)
        {
            Success = s;
            Rooms = r;
            Message = m;
        }
    }
}
