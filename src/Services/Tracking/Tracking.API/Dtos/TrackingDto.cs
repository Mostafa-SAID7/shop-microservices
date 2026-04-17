namespace Tracking.API.Dtos;

public record TrackingDto(
    Guid OrderId,
    string Status,
    string? TrackingNumber,
    string? Carrier,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? EstimatedDelivery,
    string? CurrentLocation,
    List<TrackingEventDto> Events
);

public record TrackingEventDto(
    Guid Id,
    string EventType,
    string Description,
    string? Location,
    DateTime Timestamp
);
