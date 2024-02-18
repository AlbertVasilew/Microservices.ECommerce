using Coupons.Data;
using Coupons.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Handlers.Get
{
    public class GetHandler : IRequestHandler<GetRequest, List<Coupon>>
    {
        private readonly AppDbContext dbContext;

        public GetHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Coupon>> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            return await dbContext.Coupons.ToListAsync(cancellationToken);
        }
    }
}