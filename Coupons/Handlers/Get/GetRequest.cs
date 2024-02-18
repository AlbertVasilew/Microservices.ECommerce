using Coupons.Data.Models;
using MediatR;

namespace Coupons.Handlers.Get
{
    public class GetRequest : IRequest<List<Coupon>>
    {
    }
}