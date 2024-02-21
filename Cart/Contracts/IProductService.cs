using Cart.Dtos;

namespace Cart.Contracts
{
    public interface IProductService
    {
        Task<IList<ProductDto>> GetProducts();
    }
}