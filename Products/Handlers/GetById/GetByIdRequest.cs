using MediatR;
using Products.Data.Models;

namespace Products.Handlers.GetById
{
    public class GetByIdRequest : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
