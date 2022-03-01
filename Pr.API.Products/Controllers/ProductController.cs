using Microsoft.AspNetCore.Mvc;
using Pr.API.Common.Models.DTO;
using Pr.API.Products.Interfaces;


namespace Pr.API.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        /// <summary>
        /// Products controller
        /// </summary>
        /// <param name="productsProvider"></param>
        public ProductController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        /// <summary>
        /// Getting products
        /// </summary>
        /// <returns>List of products</returns>
        /// <response code="200">List of products</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductViewModel>> GetProductsAsync()
        {
            return Ok(await _productsProvider.GetProductsAsync());
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <returns>ProductViewModel</returns>
        /// <response code="200">ProductViewModel</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductViewModel>> GetProductAsync(int id)
        {
            return Ok(await _productsProvider.GetProductAsync(id));
        }
}
}