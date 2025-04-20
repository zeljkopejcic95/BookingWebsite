using AutoMapper;
using Entities.Dto.City;
using Entities.Dto.Hotel;
using Entities.Dto.Reservation;
using Entities.Dto.Room;
using Entities.Dto.User;
using Entities.Models;

namespace BookingWebsite;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserLoginDto, User>();
        CreateMap<UserForUpdateDto, User>();

        CreateMap<City, CityDto>();
        CreateMap<CityForCreationDto, City>();
        CreateMap<CityForUpdateDto, City>();

        CreateMap<Hotel, HotelDto>();
        CreateMap<HotelForCreationDto, Hotel>();
        CreateMap<HotelForUpdateDto, Hotel>();

        CreateMap<Reservation, ReservationDto>()
            .ForMember(res => res.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
        CreateMap<ReservationForCreationDto, Reservation>();
        CreateMap<ReservationForUpdateDto, Reservation>();

        CreateMap<Room, RoomDto>();
        CreateMap<RoomForCreationDto, Room>();
        CreateMap<RoomForUpdateDto, Room>();
    }
}
