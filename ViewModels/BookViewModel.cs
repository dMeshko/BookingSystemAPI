using AutoMapper;
using BookingSystemAPI.Models;
using FluentValidation;

namespace BookingSystemAPI.ViewModels
{
    /// <summary>
    /// Book request model
    /// </summary>
    public class BookReq
    {
        public string OptionCode { get; set; }
        public SearchReq SearchReq { get; set; }
    }

    /// <summary>
    /// Book request model validator
    /// </summary>
    public class BookReqValidator : AbstractValidator<BookReq>
    {
        public BookReqValidator()
        {
            RuleFor(x => x.OptionCode)
                .NotEmpty()
                .WithMessage("Please provide an option code!");

            RuleFor(x => x.SearchReq)
                .NotEmpty()
                .WithMessage("Please provide search request");
        }
    }

    /// <summary>
    /// Book response model
    /// </summary>
    public class BookRes
    {
        public string BookingCode { get; set; }
        public DateTime BookingTime { get; set; }
    }

    /// <summary>
    /// Book response mapping profile
    /// </summary>
    public class BookResProfile : Profile
    {
        public BookResProfile()
        {
            CreateMap<Booking, BookRes>();
        }
    }
}
