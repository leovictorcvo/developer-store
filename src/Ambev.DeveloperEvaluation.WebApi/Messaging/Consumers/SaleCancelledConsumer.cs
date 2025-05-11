using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Consumers;

public class SaleCancelledConsumer : IHandleMessages<SaleCancelledEvent>
{
    private readonly ILogger _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCancelledEvent message)
    {
        _logger.LogInformation("Consuming SaleCancelledEvent: {Message}", JsonSerializer.Serialize(message));

        return Task.CompletedTask;
    }
}