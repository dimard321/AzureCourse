using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pr.API.Common.Models.DTO;

namespace Pr.API.Search.Interfaces
{
    public interface ISearchService
    {
        Task<ProductViewModel> SearchAsync(int productId);
    }
}
