using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Consumers;

public class ItemCancelledConsumer : IHandleMessages<ItemCancelledEvent>
{
    private readonly ILogger _logger;

    public ItemCancelledConsumer(ILogger<ItemCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Handle(ItemCancelledEvent message)
    {
        _logger.LogInformation("Consuming ItemCancelledEvent: {Message}", JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }
}