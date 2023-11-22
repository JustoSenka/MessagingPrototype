using MessagingPrototype.Infrastructure;

namespace MessagingPrototype.GrpcApi;

public class Worker : BackgroundService
{
    private readonly GrpcApplication _grpcApplication;

    public Worker(GrpcApplication grpcApplication) : base()
    {
        _grpcApplication = grpcApplication;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _grpcApplication.StartServer(stoppingToken);

        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _grpcApplication.Stop(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}
