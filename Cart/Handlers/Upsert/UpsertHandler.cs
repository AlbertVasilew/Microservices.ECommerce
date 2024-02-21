using Cart.Data;
using Cart.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cart.Handlers.Upsert
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
            var header = await dbContext.Headers
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (header == null)
            {
                header = new Header { UserId = request.UserId };
                await dbContext.Headers.AddAsync(header, cancellationToken);
            }
            else
            {
                var requestItemsProductsIds = request.Items.Select(x => x.ProductId).ToList();

                dbContext.Items.RemoveRange(header.Items.Where(
                    x => !requestItemsProductsIds.Contains(x.ProductId)).ToList());
            }

            foreach(var item in request.Items)
            {
                var existingItem = header.Items.FirstOrDefault(x => x.ProductId == item.ProductId);
                
                if (existingItem == null)
                {
                    existingItem = new Item { ProductId = item.ProductId };
                    header.Items.Add(existingItem);
                } 

                existingItem.Count = item.Count;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}