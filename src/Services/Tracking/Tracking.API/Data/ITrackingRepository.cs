using Tracking.API.Models;

namespace Tracking.API.Data;

public interface ITrackingRepository
{
    Task<ShipmentStatus?> GetTrackingAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<ShipmentStatus> CreateTrackingAsync(ShipmentStatus shipment, CancellationToken cancellationToken = default);
    Task<ShipmentStatus> UpdateTrackingAsync(ShipmentStatus shipment, CancellationToken cancellationToken = default);
    Task DeleteTrackingAsync(Guid orderId, CancellationToken cancellationToken = default);
}
