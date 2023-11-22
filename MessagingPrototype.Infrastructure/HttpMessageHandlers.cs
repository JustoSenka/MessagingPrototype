using MediatR;
using MessagingPrototype.HttpMessages;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MessagingPrototype.Infrastructure;

[ApiController]
[Route("api")]
public class HttpMessageHandlers : ControllerBase
{
    private readonly ISender _sender;

    public HttpMessageHandlers(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("new-order")]
    [ProducesResponseType(typeof(OrdersResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateNewOrder(CreateNewOrderRequest request, CancellationToken token)
    {
        var domainResponse = await _sender.Send(new Domain.CreateNewOrderRequest()
        {
            Id = request.Id,
            Content = request.Content,
        }, token);

        return Ok(new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        });
    }

    [HttpPut("amend-order")]
    [ProducesResponseType(typeof(OrdersResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AmendOrder(AmendOrderRequest request, CancellationToken token)
    {
        var domainResponse = await _sender.Send(new Domain.CreateNewOrderRequest()
        {
            Id = request.Id,
            Content = request.Content,
        }, token);

        return Ok(new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        });
    }

    [HttpPut("cancel-order")]
    [ProducesResponseType(typeof(OrdersResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CancelOrder(CancelOrderRequest request, CancellationToken token)
    {
        var domainResponse = await _sender.Send(new Domain.CreateNewOrderRequest()
        {
            Id = request.Id,
        }, token);

        return Ok(new OrdersResponse()
        {
            Id = domainResponse.Id,
            Message = domainResponse.Message,
        });
    }

    [HttpGet("health")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult Health(CancellationToken token)
    {
        return Ok("Ok");
    }
}
