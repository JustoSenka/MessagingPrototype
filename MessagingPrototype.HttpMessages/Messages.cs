namespace MessagingPrototype.HttpMessages;

public record CreateNewOrderRequest
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record AmendOrderRequest
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record CancelOrderRequest
{
    public string Id { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public record OrdersResponse
{
    public string Id { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
