using Cart.Data.Models;
using Cart.Dtos;
using MediatR;
using static Cart.Handlers.Get.GetCartHandler;

namespace Cart.Handlers.Get
{
    public class GetCartRequest : IRequest<CartDto>
    {
        public string UserId { get; set; }
    }
}