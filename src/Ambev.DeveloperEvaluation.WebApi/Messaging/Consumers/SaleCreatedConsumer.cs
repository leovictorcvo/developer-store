using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Consumers;

public class SaleCreatedConsumer : IHandleMessages<SaleCreatedEvent>
{
    private readonly ILogger _logger;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent message)
    {
        _logger.LogInformation("Consuming SaleCreatedEvent: {Message}", JsonSerializer.Serialize(message));

        return Task.CompletedTask;
    }
}