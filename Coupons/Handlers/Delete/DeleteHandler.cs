using Coupons.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Handlers.Delete
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
    {
        private readonly AppDbContext dbContext;

        public DeleteHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            await dbContext.Coupons.Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}