using System.Diagnostics;
using AutoMapper;
using BookingSystemAPI.Models;
using FluentValidation;

namespace BookingSystemAPI.ViewModels
{
    /// <summary>
    /// Search request model
    /// </summary>
    public class SearchReq
    {
        public string Destination { get; set; } = null!;
        public string? DepartureAirport { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    /// <summary>
    /// Search request model validator
    /// </summary>
    public class SearchReqValidator : AbstractValidator<SearchReq>
    {
        public SearchReqValidator()
        {
            RuleFor(x => x.Destination)
                .NotEmpty()
                .WithMessage("Please provide a destination!");

            RuleFor(x => x.FromDate)
                .NotEmpty()
                .WithMessage("Please provide starting date!")
                .Must(x => x > DateTime.UtcNow)
                .WithMessage("The starting date can not be in the past!")
                .Must((x, y) => x.ToDate >= y)
                .WithMessage("The end date can not be less than the starting date!");

            RuleFor(x => x.ToDate)
                .NotEmpty()
                .WithMessage("Please provide end date!")
                .Must(x => x > DateTime.UtcNow)
                .WithMessage("The end date can not be in the past!");
        }
    }

    /// <summary>
    /// Search response model
    /// </summary>
    public class SearchRes
    {
        public List<Option> Options { get; set; } = new();
    }

    /// <summary>
    /// Booking option model
    /// </summary>
    [DebuggerDisplay("OptionCode={OptionCode} HotelCode={HotelCode} FlightCode={FlightCode} ArrivalAirport={ArrivalAirport} Price={Price}")]
    public class Option
    {
        public string OptionCode { get; set; }
        public string HotelCode { get; set; }
        public string FlightCode { get; set; }
        public string ArrivalAirport { get; set; }
        public double Price { get; set; }
    }

    /// <summary>
    /// Booking option mapping profile
    /// </summary>
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<HotelDto, Option>()
                .ForMember(x => x.OptionCode,
                    y => y.MapFrom(q => q.Id))
                .ForMember(x => x.HotelCode, 
                    y => y.MapFrom(q => q.HotelCode));

            CreateMap<FlightDto, Option>()
                .ForMember(x => x.OptionCode,
                    y => y.MapFrom(q => q.FlightNumber))
                .ForMember(x => x.FlightCode,
                    y => y.MapFrom(q => q.FlightCode))
                .ForMember(x => x.ArrivalAirport,
                    y => y.MapFrom(q => q.ArrivalAirport));
        }
    }
}
