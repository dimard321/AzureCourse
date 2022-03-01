using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pr.API.Common.Domain;
using Pr.API.Common.Exceptions;
using Pr.API.Common.Models.DTO;
using Pr.API.Products.Db;
using Pr.API.Products.Interfaces;

namespace Pr.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (_dbContext.Products.Any()) return;
            _dbContext.Products.Add(new ProductEntity() {Id = 1, Name = "Keyboard", Price = 20, Inventory = 100});
            _dbContext.Products.Add(new ProductEntity() {Id = 2, Name = "Monitor", Price = 200, Inventory = 50});
            _dbContext.Products.Add(new ProductEntity() {Id = 3, Name = "Mouse", Price = 10, Inventory = 40});
            _dbContext.Products.Add(new ProductEntity() {Id = 4, Name = "Headphones", Price = 50, Inventory = 15});
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            var productEntities = await _dbContext.Products.ToListAsync();

            return _mapper.Map<List<ProductViewModel>>(productEntities);
        }

        public async Task<ProductViewModel> GetProductAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(pr => pr.Id == id);
            if (product is null)
            {
                throw new NotFoundException($"ProductEntity with id = {id} not found");
            }
            return _mapper.Map<ProductViewModel>(product);
        }
    }
}
