using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Data.Models;

namespace Products.Handlers.Upsert
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
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                product = new Product();
                await dbContext.AddAsync(product);
            }

            product.Price = request.Price;
            product.Name = request.Name;
            product.ImageUrl = request.ImageUrl;

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}