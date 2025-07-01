using AutoMapper;
using RentalHome.Application.Models.Booking;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<CreateBookingModel, Booking>();
        CreateMap<UpdateBookingModel, Booking>();
        CreateMap<BookingModel, Booking>().ReverseMap(); 
        
    }
}