using MediatR;

namespace Products.Handlers.Upsert
{
    public class UpsertRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}