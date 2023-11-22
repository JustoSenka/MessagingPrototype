using Grpc.Core;
using MessagingPrototype.GrpcMessages;
using Newtonsoft.Json;
using System.Text;

namespace Tests;

public class Tests
{
    private const string ServerAddress = "localhost";
    private const int Port = 50051;

    [Fact]
    public void CreateNewOrderViaGrpc()
    {
        var channel = new Channel($"{ServerAddress}:{Port}", ChannelCredentials.Insecure);
        var client = new Orders.OrdersClient(channel);

        var orderId = Guid.NewGuid().ToString();
        var request = new CreateNewOrderRequest
        {
            Id = orderId,
            Content = $"Test: {nameof(CreateNewOrderViaGrpc)}"
        };

        var response = client.CreateNewOrder(request);
        channel.ShutdownAsync().Wait();

        Assert.Equal(orderId, response.Id);
        Assert.Equal("Ok", response.Message);
    }

    [Fact]
    public async Task CreateNewOrderViaHttp()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var orderId = Guid.NewGuid().ToString();
        var request = new CreateNewOrderRequest
        {
            Id = orderId,
            Content = $"Test: {nameof(CreateNewOrderViaGrpc)}"
        };

        var query = "api/new-order";
        var stringContents = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync(query, stringContents);
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        Console.WriteLine(httpResponseString);

        var response = JsonConvert.DeserializeObject<OrdersResponse>(httpResponseString)!;

        Assert.Equal(orderId, response.Id);
        Assert.Equal("Ok", response.Message);
    }

    [Fact]
    public async Task CheckHealthViaHttp()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var httpResponse = await client.GetAsync("api/health");
        var httpResponseString = await httpResponse.Content.ReadAsStringAsync();

        Console.WriteLine("Response: " + httpResponseString);

        Assert.Equal("Ok", httpResponseString);
    }
}
