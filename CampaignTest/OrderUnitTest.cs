using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using Campaign.Core.Services.OrderUseCases;
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
    public class CreateOrderHandlerTest
    {
        [Fact]
        public void CreateOrderHandlerData_ShouldbeTrue_WhenCreated()
        {
            IRepository<Order> repository = new Mock<IRepository<Order>>().Object;
            ILogger<CreateOrderHandler> logger = new Mock<ILogger<CreateOrderHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            CreateOrderHandler campaignHandler = new CreateOrderHandler(repository, logger, mediator);

            var result = campaignHandler.Handle(new Dtos.Requests.CreateOrderRequest { ProductCode = "P1", Price = 10, Quantity = 5 }, new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data);
        }

        [Fact]
        public void GetOrderHandler_ShouldGetProduct()
        {
            IQueryable<Order> prd = new List<Order>
            {
                new Order{ ProductCode = "P1", Price = 10, Quantity = 5 }
            }.AsQueryable();

            var repoMock = new Mock<IRepository<Order>>();
            repoMock.Setup(x => x.GetWhereAsync(It.IsAny<Expression<Func<Order, bool>>>())).Returns<Expression<Func<Order, bool>>>(predicate => Task.FromResult(prd.Where(predicate).ToList().AsEnumerable()));
            IRepository<Order> repository = repoMock.Object;
            ILogger<GetOrderHandler> logger = new Mock<ILogger<GetOrderHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            GetOrderHandler productHandler = new GetOrderHandler(repository, logger);

            var result = productHandler.Handle(new Dtos.Requests.GetOrderRequest("P1"), new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data != null);
        }
    }
}
