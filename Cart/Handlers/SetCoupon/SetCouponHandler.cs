using Cart.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cart.Handlers.SetCoupon
{
    public class SetCouponHandler : IRequestHandler<SetCouponRequest, Unit>
    {
        private readonly AppDbContext dbContext;

        public SetCouponHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetCouponRequest request, CancellationToken cancellationToken)
        {
            var header = await dbContext.Headers.FirstOrDefaultAsync(x => x.Id == request.HeaderId, cancellationToken);

            if (header != null)
            {
                header.CouponCode = request.CouponCode;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}