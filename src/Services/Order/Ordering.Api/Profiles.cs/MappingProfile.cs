using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Ordering.Api.Profiles.cs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckoutEvent,CheckOutOrderCommand>().ReverseMap();
        }
    }
}
