using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using Campaign.Core.Services.CampaignUseCases;
using Campaign.Core.Services.ProdutcUseCases;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Campaign.Core.Test
{
    public class CreateProductHandlerTest
    {
        [Fact]
        public void CreateProductHandlerData_ShouldbeTrue_WhenCreated()
        {
            IRepository<Product> repository = new Mock<IRepository<Product>>().Object;
            ILogger<CreateProductHandler> logger = new Mock<ILogger<CreateProductHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            CreateProductHandler productHandler = new CreateProductHandler(repository, logger, mediator);

            var result = productHandler.Handle(new Dtos.Requests.CreateProductRequest { ProductCode = "P1", Price = 10, Stock = 10 }, new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data);
        }

        [Fact]
        public void GetProductHandler_ShouldGetProduct()
        {
            IQueryable<Product> product = new List<Product>
            {
                new Product{ProductCode = "P1", Price=10,Stock=10}
            }.AsQueryable();

            IQueryable<Campaigning> campaigning = new List<Campaigning>
            {
                new Campaigning{CampaignName = "C1", ProductCode = "P1", Duration = 5, Limit = 10, TargetSalesCount = 10}
            }.AsQueryable();

            var repoMock = new Mock<IRepository<Product>>();
            repoMock.Setup(x => x.GetWhereAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns<Expression<Func<Product, bool>>>(predicate => Task.FromResult(product.Where(predicate).ToList().AsEnumerable()));
            IRepository<Product> repository = repoMock.Object;
            ILogger<GetProductHandler> logger = new Mock<ILogger<GetProductHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            GetProductHandler productHandler = new GetProductHandler(repository, logger, mediator);

            var result = productHandler.Handle(new Dtos.Requests.GetProductRequest("P1"), new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data != null);
        }
    }
}
