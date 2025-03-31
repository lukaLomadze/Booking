using AutoMapper;
using Booking.Models;
using Booking.Models.DTOs;
using Booking.Models.DTOs.Hotel;
using Booking.Models.DTOs.Image;
using Booking.Models.DTOs.Reservation;
using Booking.Models.DTOs.Room;
using Booking.Models.Entities;

namespace Booking
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, AddHotelDTO>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
            CreateMap<Room, UpdateRoomDTO>().ReverseMap();
            CreateMap<Room, AddRoomDTO>().ReverseMap();
            CreateMap<Room, Room>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Image, AddImageDTO>().ReverseMap();

        }
    }
}
