using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pr.API.Common.Models.DTO;
using Pr.API.Search.Interfaces;

namespace Pr.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProductsService _productsService;

        public SearchService(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<ProductViewModel> SearchAsync(int productId)
        {
            await Task.Delay(1);

            return await _productsService.GetProductAsync(productId);
        }
    }
}
