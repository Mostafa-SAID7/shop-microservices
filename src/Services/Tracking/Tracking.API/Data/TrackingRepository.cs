using Marten;
using Tracking.API.Models;

namespace Tracking.API.Data;

public class TrackingRepository : ITrackingRepository
{
    private readonly IDocumentSession _session;

    public TrackingRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<ShipmentStatus?> GetTrackingAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _session.LoadAsync<ShipmentStatus>(orderId, cancellationToken);
    }

    public async Task<ShipmentStatus> CreateTrackingAsync(ShipmentStatus shipment, CancellationToken cancellationToken = default)
    {
        _session.Store(shipment);
        await _session.SaveChangesAsync(cancellationToken);
        return shipment;
    }

    public async Task<ShipmentStatus> UpdateTrackingAsync(ShipmentStatus shipment, CancellationToken cancellationToken = default)
    {
        shipment.UpdatedAt = DateTime.UtcNow;
        _session.Update(shipment);
        await _session.SaveChangesAsync(cancellationToken);
        return shipment;
    }

    public async Task DeleteTrackingAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        _session.Delete<ShipmentStatus>(orderId);
        await _session.SaveChangesAsync(cancellationToken);
    }
}
