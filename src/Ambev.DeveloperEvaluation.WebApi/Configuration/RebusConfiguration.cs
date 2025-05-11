using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.WebApi.Messaging;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.WebApi.Configuration
{
    public static class RebusConfiguration
    {
        public static void AddRebusMessaging(this IServiceCollection services)
        {
            services.AddRebus((configure, provider) => configure
                .Logging(l => l.Console())
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(true), "carts_queue"))
                .Routing(r => r.TypeBased()
                    .Map<SaleCreatedEvent>("carts.sale.created")
                    .Map<SaleModifiedEvent>("carts.sale.modified")
                    .Map<SaleCancelledEvent>("carts.sale.cancelled")
                    .Map<ItemCancelledEvent>("carts.sale_item.cancelled"))
                , onCreated: async bus =>
                {
                    await bus.Subscribe<SaleCreatedEvent>();
                    await bus.Subscribe<SaleModifiedEvent>();
                    await bus.Subscribe<SaleCancelledEvent>();
                    await bus.Subscribe<ItemCancelledEvent>();
                }
            );
            services.AutoRegisterHandlersFromAssemblyOf<Program>();
            services.AddScoped<IEventNotification, RebusMessageProducer>();
        }
    }
}