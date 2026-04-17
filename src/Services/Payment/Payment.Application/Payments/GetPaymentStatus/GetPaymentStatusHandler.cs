using Payment.Infrastructure.Data;

namespace Payment.Application.Payments.GetPaymentStatus;

public record GetPaymentStatusQuery(Guid TransactionId) : IQuery<GetPaymentStatusResult>;
public record GetPaymentStatusResult(Guid TransactionId, string Status, decimal Amount, DateTime CreatedAt);

public class GetPaymentStatusQueryValidator : AbstractValidator<GetPaymentStatusQuery>
{
    public GetPaymentStatusQueryValidator()
    {
        RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId is required");
    }
}

public class GetPaymentStatusQueryHandler(IPaymentRepository repository)
    : IQueryHandler<GetPaymentStatusQuery, GetPaymentStatusResult>
{
    public async Task<GetPaymentStatusResult> Handle(GetPaymentStatusQuery query, CancellationToken cancellationToken)
    {
        var payment = await repository.GetPaymentAsync(query.TransactionId, cancellationToken);
        
        if (payment == null)
            throw new Exception($"Payment transaction {query.TransactionId} not found");

        return new GetPaymentStatusResult(payment.Id, payment.Status, payment.Amount, payment.CreatedAt);
    }
}
