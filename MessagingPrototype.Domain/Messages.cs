using MediatR;

namespace MessagingPrototype.Domain;

public record CreateNewOrderRequest : IRequest<OrdersResponse>
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record AmendOrderRequest : IRequest<OrdersResponse>
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record CancelOrderRequest : IRequest<OrdersResponse>
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record OrdersResponse : IRequest<OrdersResponse>
{
    public string Id { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
