using Payment.Domain.Entities;

namespace Payment.Infrastructure.Data;

public interface IPaymentRepository
{
    Task<PaymentTransaction?> GetPaymentAsync(Guid transactionId, CancellationToken cancellationToken = default);
    Task<PaymentTransaction> CreatePaymentAsync(PaymentTransaction payment, CancellationToken cancellationToken = default);
    Task<PaymentTransaction> UpdatePaymentAsync(PaymentTransaction payment, CancellationToken cancellationToken = default);
    Task<PaymentRefund> CreateRefundAsync(PaymentRefund refund, CancellationToken cancellationToken = default);
}
