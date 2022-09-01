using AutoMapper;
using Booking_app.Data;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Booking_app.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Hotel, HotelVM>().ReverseMap();
            CreateMap<HotelDTO, HotelVM>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();

            CreateMap<Rezervation, RezervationVM>().ReverseMap();
            CreateMap<RezervationDTO, RezervationVM>().ReverseMap();
            CreateMap<UserRezervationVM, RezervationDTO>().ReverseMap();
            CreateMap<RoomsRezervationVM, RezervationDTO>().ReverseMap();
            CreateMap<Rezervation, RezervationDTO>().ReverseMap();

            CreateMap<Room, RoomVM>().ReverseMap();
            CreateMap<RoomDTO, RoomRezervationVM>().ReverseMap();
            CreateMap<RoomDTO, RoomVM>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<User, AdminVM>().ReverseMap();
            CreateMap<UserVM, AdminVM>().ReverseMap();
            CreateMap<UserDTO, AdminVM>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserVM, UserDTO>().ReverseMap();

            CreateMap<RoomTypeDTO, RoomTypeVM>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
            CreateMap<RoomType, RoomTypeVM>().ReverseMap();
        }
    }
}
