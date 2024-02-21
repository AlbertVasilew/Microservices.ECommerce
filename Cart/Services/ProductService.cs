using Cart.Contracts;
using Cart.Dtos;
using Newtonsoft.Json;

namespace Cart.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ProductDto>> GetProducts()
        {
            var client = httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/products");
            var content = await response.Content.ReadAsStringAsync();

            var products = new List<ProductDto>();

            if (response.IsSuccessStatusCode)
                products = JsonConvert.DeserializeObject<IList<ProductDto>>(content)?.ToList();

            return products;
        }
    }
}