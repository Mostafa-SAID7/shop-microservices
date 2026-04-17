using BuildingBlocks.Messaging.Events;
using MassTransit;
using Tracking.API.Data;
using Tracking.API.Models;

namespace Tracking.API.EventHandlers;

public class OrderCreatedEventHandler(ITrackingRepository repository, ILogger<OrderCreatedEventHandler> logger)
    : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var shipment = new ShipmentStatus
        {
            OrderId = context.Message.Id,
            Status = "Processing",
            CreatedAt = DateTime.UtcNow,
            Events = new List<TrackingEvent>
            {
                new TrackingEvent
                {
                    EventType = "OrderCreated",
                    Description = $"Order created for customer {context.Message.CustomerId}",
                    Timestamp = DateTime.UtcNow
                }
            }
        };

        await repository.CreateTrackingAsync(shipment, context.CancellationToken);
    }
}

public record OrderCreatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string OrderName { get; set; } = default!;
    public decimal TotalPrice { get; set; }
}
