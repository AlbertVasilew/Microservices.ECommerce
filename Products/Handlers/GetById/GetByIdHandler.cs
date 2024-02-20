using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Data.Models;

namespace Products.Handlers.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, Product>
    {
        private readonly AppDbContext dbContext;

        public GetByIdHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
