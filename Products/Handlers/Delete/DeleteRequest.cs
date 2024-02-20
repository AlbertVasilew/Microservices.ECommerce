using MediatR;

namespace Products.Handlers.Delete
{
    public class DeleteRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}