using Carter;
using Mediator;

namespace Tracking.API.Tracking.GetTracking;

public class GetTrackingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tracking/{orderId}", GetTracking)
            .WithName("GetTracking")
            .WithOpenApi()
            .Produces<GetTrackingResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get tracking information for an order")
            .WithDescription("Retrieve current tracking status and event history for a specific order");
    }

    private async Task<IResult> GetTracking(Guid orderId, ISender sender)
    {
        var result = await sender.Send(new GetTrackingQuery(orderId));
        
        if (result.Tracking == null)
            return Results.NotFound($"Tracking not found for order {orderId}");

        return Results.Ok(result);
    }
}
