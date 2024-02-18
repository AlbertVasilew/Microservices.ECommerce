using Coupons.Data;
using Coupons.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Handlers.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, Coupon?>
    {
        private readonly AppDbContext dbContext;

        public GetByIdHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Coupon?> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            return await dbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}