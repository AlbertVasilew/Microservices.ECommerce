using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Data.Models;

namespace Products.Handlers.Get
{
    public class GetHandler : IRequestHandler<GetRequest, List<Product>>
    {
        private readonly AppDbContext dbContext;

        public GetHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            return await dbContext.Products.ToListAsync(cancellationToken);
        }
    }
}