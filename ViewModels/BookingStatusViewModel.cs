using AutoMapper;
using BookingSystemAPI.Models;
using FluentValidation;
using System.Text.Json.Serialization;

namespace BookingSystemAPI.ViewModels
{
    /// <summary>
    /// Check booking status request model
    /// </summary>
    public class CheckStatusReq
    {
        public string BookingCode { get; set; }
    }

    /// <summary>
    /// Check booking status request model validator
    /// </summary>
    public class CheckStatusReqValidator : AbstractValidator<CheckStatusReq>
    {
        public CheckStatusReqValidator()
        {
            RuleFor(x => x.BookingCode)
                .NotEmpty()
                .WithMessage("Please provide a booking code!");
        }
    }

    /// <summary>
    /// Check booking response model
    /// </summary>
    public class CheckStatusRes
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatusEnum Status { get; set; } = BookingStatusEnum.Pending;
    }

    /// <summary>
    /// Booking response status
    /// </summary>
    public enum BookingStatusEnum
    {
        Success,
        Failed,
        Pending
    }

    /// <summary>
    /// Check booking response mapping profile
    /// </summary>
    public class CheckStatusResProfile : Profile
    {
        public CheckStatusResProfile()
        {
            CreateMap<Booking, CheckStatusRes>()
                .ForMember(x => x.Status,
                    y => y.MapFrom(q =>
                        q.BookingTime.AddSeconds(q.SleepTime) <= DateTime.UtcNow
                            ? q.BookingType == BookingType.Normal ? BookingStatusEnum.Success : BookingStatusEnum.Failed
                            : BookingStatusEnum.Pending));
        }
    }
}
