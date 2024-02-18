using MediatR;

namespace Coupons.Handlers.Upsert
{
    public class UpsertRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
    }
}