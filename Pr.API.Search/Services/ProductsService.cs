using System.Text.Json;
using Pr.API.Common.Models.DTO;
using Pr.API.Search.Interfaces;

namespace Pr.API.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ProductViewModel> GetProductAsync(int productId)
        {
            var client = _httpClientFactory.CreateClient("OrdersServices");
            var response = await client.GetAsync($"https://localhost:7105/api/products/{productId}");
            var content = await response.Content.ReadAsByteArrayAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<ProductViewModel>(content, options) ?? throw new InvalidOperationException();
            return result;
        }
    }
}
