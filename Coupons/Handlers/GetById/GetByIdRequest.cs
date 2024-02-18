using Coupons.Data.Models;
using MediatR;

namespace Coupons.Handlers.GetById
{
    public class GetByIdRequest : IRequest<Coupon?>
    {
        public int Id { get; set; }
    }
}