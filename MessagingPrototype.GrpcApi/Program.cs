// See https://aka.ms/new-console-template for more information

using MessagingPrototype.Infrastructure;
using MessagingPrototype.Application;
using MessagingPrototype.GrpcApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<MessageHandlers>();
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddSingleton<GrpcApplication>();
builder.Services.AddSingleton<GrpcMessageHandlers>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.Run();
