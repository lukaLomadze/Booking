using Booking.Models.Entities;

namespace Booking.Responses.RoomResponse
{
    public class GetRoomTypesResponse
    {
        public bool Success { get; set; }
        public List<RoomType> Types { get; set; }
        public string Message { get; set; }
        public GetRoomTypesResponse(bool s, List<RoomType> t, string m)
        {
            Success = s;
            Types = t;
            Message = m;
        }
    }
}
