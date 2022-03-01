using Pr.API.Common.Models.DTO;

namespace Pr.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<IEnumerable<ProductViewModel>>GetProductsAsync();
        Task<ProductViewModel> GetProductAsync(int id);
    }
}
