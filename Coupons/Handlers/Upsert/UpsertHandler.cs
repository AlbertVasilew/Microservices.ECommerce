using Coupons.Data;
using Coupons.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Handlers.Upsert
{
    public class UpsertHandler : IRequestHandler<UpsertRequest, Unit>
    {
        private readonly AppDbContext dbContext;

        public UpsertHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Unit> Handle(UpsertRequest request, CancellationToken cancellationToken)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (coupon == null)
            {
                coupon = new Coupon();
                await dbContext.AddAsync(coupon);
            }

            coupon.Code = request.Code;
            coupon.Discount = request.Discount;

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}