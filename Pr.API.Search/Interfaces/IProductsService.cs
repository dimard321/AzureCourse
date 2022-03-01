using Pr.API.Common.Models.DTO;

namespace Pr.API.Search.Interfaces
{
    public interface IProductsService
    {
        Task<ProductViewModel> GetProductAsync(int productId);
    }
}
