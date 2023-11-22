using MediatR;
using MessagingPrototype.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MessagingPrototype.Application;

public class MessageHandlers :
    IRequestHandler<CreateNewOrderRequest, OrdersResponse>,
    IRequestHandler<AmendOrderRequest, OrdersResponse>,
    IRequestHandler<CancelOrderRequest, OrdersResponse>
{
    private readonly ILogger<MessageHandlers> _logger;

    public MessageHandlers(ILogger<MessageHandlers> logger)
    {
        _logger = logger;
    }

    public Task<OrdersResponse> Handle(CreateNewOrderRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(JsonConvert.SerializeObject(request));
        
        // Database code etc

        return Task.FromResult(new OrdersResponse()
        {
            Id = request.Id,
            Message = "Ok"
        });
    }

    public Task<OrdersResponse> Handle(AmendOrderRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(JsonConvert.SerializeObject(request));

        // Database code etc

        return Task.FromResult(new OrdersResponse()
        {
            Id = request.Id,
            Message = "Ok"
        });
    }

    public Task<OrdersResponse> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(JsonConvert.SerializeObject(request));

        // Database code etc

        return Task.FromResult(new OrdersResponse()
        {
            Id = request.Id,
            Message = "Ok"
        });
    }
}
