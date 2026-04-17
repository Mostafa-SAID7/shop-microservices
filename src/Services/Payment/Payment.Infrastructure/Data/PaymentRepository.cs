using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;

namespace Payment.Infrastructure.Data;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentTransaction?> GetPaymentAsync(Guid transactionId, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentTransactions
            .Include(p => p.Refunds)
            .FirstOrDefaultAsync(p => p.Id == transactionId, cancellationToken);
    }

    public async Task<PaymentTransaction> CreatePaymentAsync(PaymentTransaction payment, CancellationToken cancellationToken = default)
    {
        _context.PaymentTransactions.Add(payment);
        await _context.SaveChangesAsync(cancellationToken);
        return payment;
    }

    public async Task<PaymentTransaction> UpdatePaymentAsync(PaymentTransaction payment, CancellationToken cancellationToken = default)
    {
        _context.PaymentTransactions.Update(payment);
        await _context.SaveChangesAsync(cancellationToken);
        return payment;
    }

    public async Task<PaymentRefund> CreateRefundAsync(PaymentRefund refund, CancellationToken cancellationToken = default)
    {
        _context.PaymentRefunds.Add(refund);
        await _context.SaveChangesAsync(cancellationToken);
        return refund;
    }
}
