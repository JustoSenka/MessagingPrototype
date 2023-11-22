using Grpc.Core;
using MessagingPrototype.GrpcMessages;

namespace MessagingPrototype.Infrastructure;

public class GrpcApplication
{
    private const string ServerAddress = "localhost";
    private const int Port = 50051;
    private Server? _server;

    private readonly GrpcMessageHandlers _grpcMessageHandlers;

    public GrpcApplication(GrpcMessageHandlers grpcMessageHandlers)
    {
        _grpcMessageHandlers = grpcMessageHandlers;
    }

    public void StartServer(CancellationToken token = default)
    {
        _server = new Server
        {
            Services = { Orders.BindService(_grpcMessageHandlers) },
            Ports = { new ServerPort(ServerAddress, Port, ServerCredentials.Insecure) }
        };

        _server.Start();

        Console.WriteLine($"Server listening on port {Port}");
    }

    public void Stop(CancellationToken token = default)
    {
        _server?.ShutdownAsync().Wait(token);
        _server = null;
    }

    public void Send(string content, CancellationToken token = default)
    {
        var channel = new Channel($"{ServerAddress}:{Port}", ChannelCredentials.Insecure);
        var client = new Orders.OrdersClient(channel);

        var request = new CreateNewOrderRequest { Id = Guid.NewGuid().ToString(), Content = content };

        try
        {
            var response = client.CreateNewOrder(request);
            Console.WriteLine($"Server response: {response.Message}");
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"Error communicating with the server: {ex.Status}");
        }

        channel.ShutdownAsync().Wait(token);
    }
}
