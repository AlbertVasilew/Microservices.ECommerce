using MediatR;

namespace Coupons.Handlers.Delete
{
    public class DeleteRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}