using Carter;
using Mediator;
using Payment.Application.Payments.GetPaymentStatus;

namespace Payment.API.Payments.GetPaymentStatus;

public class GetPaymentStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/payments/{transactionId}", GetPaymentStatus)
            .WithName("GetPaymentStatus")
            .WithOpenApi()
            .Produces<GetPaymentStatusResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get payment status")
            .WithDescription("Retrieve the status of a payment transaction");
    }

    private async Task<IResult> GetPaymentStatus(Guid transactionId, ISender sender)
    {
        var result = await sender.Send(new GetPaymentStatusQuery(transactionId));
        return Results.Ok(result);
    }
}
