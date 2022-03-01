using System;
using System.Net.Http;
using AutoFixture;
using AutoMapper;
using CST.Tests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Pr.API.Common.Domain;
using Pr.API.Common.Models.DTO;
using Pr.API.Common.Profiles;
using Pr.API.Products.Db;
using Pr.API.Products.Providers;
using Xunit;

namespace Pr.API.UnitTests
{
    public class ProductsServiceTests : AutoMockerTestsBase<ProductsProvider>
    {
        private readonly Fixture _fixture;
        private readonly ProductsProvider _productsProvider;
        private readonly Mock<ILogger<ProductsProvider>> _logger;
        private readonly ProductsDbContext _dbContext;
        private readonly Mapper _mapper;

        public ProductsServiceTests()
        {
            _fixture = FixtureInitializer.InitializeFixture();
            
            _logger = GetMock<ILogger<ProductsProvider>>();
            
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductProfile()));
            _mapper = new Mapper(configuration);

            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductById_ShouldReturnProduct))
                .Options;
            _dbContext = new ProductsDbContext(options);
        }

        [Fact]
        public void GetProductById_ShouldReturnProduct()
        {
            //Act
            var product = _fixture.Build<ProductEntity>()
                .With(pr => pr.Id, 1)
                .Create();

            var productVm = _fixture.Build<ProductViewModel>()
                .With(pr => pr.Id, product.Id)
                .Create();

            var productsProvider = new ProductsProvider(_dbContext, _logger.Object, _mapper);

            //Arrange
            var func = async () => { await productsProvider.GetProductAsync(product.Id); };
            var result = productsProvider.GetProductAsync(product.Id).Result;

            //Assert
            func.Should().NotThrowAsync();
            result.Should().BeOfType(productVm.GetType());
        }
    }
}