using Grpc.Core;
using MediatR;
using MessagingPrototype.GrpcMessages;

namespace MessagingPrototype.Infrastructure;

public class GrpcMessageHandlers : Orders.OrdersBase
{
    private readonly ISender _sender;

    public GrpcMessageHandlers(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<OrdersResponse> CreateNewOrder(CreateNewOrderRequest request, ServerCallContext context)
    {
        var domainResponse = await _sender.Send(new Domain.CreateNewOrderRequest()
        {
            Id = request.Id,
            Content = request.Content,
        });

        return new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        };
    }

    public override async Task<OrdersResponse> AmendOrder(AmendOrderRequest request, ServerCallContext context)
    {
        var domainResponse = await _sender.Send(new Domain.AmendOrderRequest()
        {
            Id = request.Id,
            Content = request.Content,
        });

        return new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        };
    }

    public override async Task<OrdersResponse> CancelOrder(CancelOrderRequest request, ServerCallContext context)
    {
        var domainResponse = await _sender.Send(new Domain.CancelOrderRequest()
        {
            Id = request.Id,
        });

        return new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        };
    }
}
