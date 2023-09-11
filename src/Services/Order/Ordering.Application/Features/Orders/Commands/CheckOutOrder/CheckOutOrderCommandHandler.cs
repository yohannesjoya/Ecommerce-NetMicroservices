using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models.EmailModels;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckOutOrderCommandHandler> _logger;

        public CheckOutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckOutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendEmail(newOrder);
            return newOrder.Id; 
        }

        private async Task SendEmail(Order order)
        {
            var email = new Email()
            {
                Body = $"Order was created. and Order id is {order.Id}",
                Subject = "Order was created.",
                To = "yohannesdestagebru10@gmail.com"
            };

            try { 
            
                await _emailService.SendEmail(email);
                _logger.LogInformation($"Order {order.Id} is successfully Emailed.");
            
            } catch (Exception ex) {
            
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
            
        }
    }
}
