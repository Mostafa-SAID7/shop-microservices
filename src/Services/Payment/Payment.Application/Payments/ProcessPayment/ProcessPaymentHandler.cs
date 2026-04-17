using Payment.Domain.Entities;
using Payment.Infrastructure.Data;

namespace Payment.Application.Payments.ProcessPayment;

public record ProcessPaymentCommand(
    Guid OrderId,
    decimal Amount,
    string CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod
) : ICommand<ProcessPaymentResult>;

public record ProcessPaymentResult(bool Success, Guid TransactionId, string Message);

public class ProcessPaymentCommandValidator : AbstractValidator<ProcessPaymentCommand>
{
    public ProcessPaymentCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required");
        RuleFor(x => x.CardName).NotEmpty().WithMessage("CardName is required");
    }
}

public class ProcessPaymentCommandHandler(IPaymentRepository repository, ILogger<ProcessPaymentCommandHandler> logger)
    : ICommandHandler<ProcessPaymentCommand, ProcessPaymentResult>
{
    public async Task<ProcessPaymentResult> Handle(ProcessPaymentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing payment for order {OrderId}", command.OrderId);

        var payment = new PaymentTransaction
        {
            OrderId = command.OrderId,
            Amount = command.Amount,
            PaymentMethod = command.PaymentMethod == 1 ? "CreditCard" : "PayPal",
            Status = "Processing",
            CardLast4 = command.CardNumber.Substring(command.CardNumber.Length - 4),
            CreatedAt = DateTime.UtcNow
        };

        // TODO: Integrate with actual payment gateway (Stripe, PayPal, etc.)
        // For now, simulate successful payment
        payment.Status = "Completed";
        payment.CompletedAt = DateTime.UtcNow;
        payment.TransactionReference = Guid.NewGuid().ToString();

        var result = await repository.CreatePaymentAsync(payment, cancellationToken);

        logger.LogInformation("Payment processed successfully for order {OrderId} with transaction {TransactionId}", 
            command.OrderId, result.Id);

        return new ProcessPaymentResult(true, result.Id, "Payment processed successfully");
    }
}
