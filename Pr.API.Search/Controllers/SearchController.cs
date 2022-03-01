using Microsoft.AspNetCore.Mvc;
using Pr.API.Common.Models.Search;
using Pr.API.Search.Interfaces;

namespace Pr.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        
        /// <summary>
        /// Search controller
        /// </summary>
        /// <param name="searchService></param>
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Search async
        /// </summary>
        /// <returns>List of products</returns>
        [HttpPost]
        public async Task<dynamic> SearchAsync(SearchTerm term)
        {
            return await _searchService.SearchAsync(term.ProductId);
        }
    }
}
