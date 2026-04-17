using Tracking.API.Data;
using Tracking.API.Dtos;

namespace Tracking.API.Tracking.GetTracking;

public record GetTrackingQuery(Guid OrderId) : IQuery<GetTrackingResult>;
public record GetTrackingResult(TrackingDto? Tracking);

public class GetTrackingQueryValidator : AbstractValidator<GetTrackingQuery>
{
    public GetTrackingQueryValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}

public class GetTrackingQueryHandler(ITrackingRepository repository)
    : IQueryHandler<GetTrackingQuery, GetTrackingResult>
{
    public async Task<GetTrackingResult> Handle(GetTrackingQuery query, CancellationToken cancellationToken)
    {
        var shipment = await repository.GetTrackingAsync(query.OrderId, cancellationToken);
        
        if (shipment == null)
            return new GetTrackingResult(null);

        var trackingDto = new TrackingDto(
            shipment.OrderId,
            shipment.Status,
            shipment.TrackingNumber,
            shipment.Carrier,
            shipment.CreatedAt,
            shipment.UpdatedAt,
            shipment.EstimatedDelivery,
            shipment.CurrentLocation,
            shipment.Events.Select(e => new TrackingEventDto(e.Id, e.EventType, e.Description, e.Location, e.Timestamp)).ToList()
        );

        return new GetTrackingResult(trackingDto);
    }
}
