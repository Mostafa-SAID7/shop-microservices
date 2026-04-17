namespace Tracking.API.Models;

public class ShipmentStatus
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Processing, Shipped, InTransit, Delivered, Cancelled
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? EstimatedDelivery { get; set; }
    public string? CurrentLocation { get; set; }
    public List<TrackingEvent> Events { get; set; } = new();
}

public class TrackingEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EventType { get; set; } = default!; // Created, Shipped, InTransit, Delivered, etc.
    public string Description { get; set; } = default!;
    public string? Location { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
