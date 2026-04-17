using Carter;
using Mediator;
using Payment.Application.Payments.ProcessPayment;

namespace Payment.API.Payments.ProcessPayment;

public record ProcessPaymentRequest(
    Guid OrderId,
    decimal Amount,
    string CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod
);

public class ProcessPaymentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/payments/process", ProcessPayment)
            .WithName("ProcessPayment")
            .WithOpenApi()
            .Produces<ProcessPaymentResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Process a payment")
            .WithDescription("Process payment for an order");
    }

    private async Task<IResult> ProcessPayment(ProcessPaymentRequest request, ISender sender)
    {
        var command = new ProcessPaymentCommand(
            request.OrderId,
            request.Amount,
            request.CardName,
            request.CardNumber,
            request.Expiration,
            request.CVV,
            request.PaymentMethod
        );

        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}
