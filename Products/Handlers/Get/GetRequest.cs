using MediatR;
using Products.Data.Models;

namespace Products.Handlers.Get
{
    public class GetRequest : IRequest<List<Product>>
    {
    }
}
