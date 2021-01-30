using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using Campaign.Core.Services.CampaignUseCases;
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
    public class CreateCampaignHandlerTest
    {
        [Fact]
        public void CreateCampaignHandlerData_ShouldbeTrue_WhenCreated()
        {
            IRepository<Campaigning> repository = new Mock<IRepository<Campaigning>>().Object;
            ILogger<CreateCampaignHandler> logger = new Mock<ILogger<CreateCampaignHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            CreateCampaignHandler campaignHandler = new CreateCampaignHandler(repository, logger, mediator);

            var result = campaignHandler.Handle(new Dtos.Requests.CreateCampaignRequest { CampaignName = "C1", ProductCode = "P1", Duration = 5, Limit = 10, TargetSalesCount = 10 }, new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data);

        }

        [Fact]
        public void GetCampaignHandler_ShouldGetCampaign()
        {
            IQueryable<Campaigning> prd = new List<Campaigning>
            {
                new Campaigning{CampaignName = "C1", ProductCode = "P1", Duration = 5, Limit = 10, TargetSalesCount = 10}
            }.AsQueryable();

            var repoMock = new Mock<IRepository<Campaigning>>();
            repoMock.Setup(x => x.GetWhereAsync(It.IsAny<Expression<Func<Campaigning, bool>>>())).Returns<Expression<Func<Campaigning, bool>>>(predicate => Task.FromResult(prd.Where(predicate).ToList().AsEnumerable()));
            IRepository<Campaigning> repository = repoMock.Object;
            ILogger<GetCampaignHandler> logger = new Mock<ILogger<GetCampaignHandler>>().Object;
            IMediator mediator = new Mock<IMediator>().Object;

            GetCampaignHandler campaignHandler = new GetCampaignHandler(repository, logger);

            var result = campaignHandler.Handle(new Dtos.Requests.GetCampaignRequest("C1"), new System.Threading.CancellationToken()).Result;

            Assert.True(result.Data != null);
        }
    }
}
