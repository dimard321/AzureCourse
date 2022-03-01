using Microsoft.EntityFrameworkCore;
using Pr.API.Common.Domain;


namespace Pr.API.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ProductsDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
