using Cart.Dtos;
using MediatR;

namespace Cart.Handlers.Upsert
{
    public class UpsertRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
        public IList<ItemDto> Items { get; set; }
    }
}