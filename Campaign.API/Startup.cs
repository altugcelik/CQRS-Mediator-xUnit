using Campaign.Core.Interfaces;
using Campaign.Core.Services.CampaignUseCases;
using Campaign.Core.Services.OrderUseCases;
using Campaign.Core.Services.ProdutcUseCases;
using Campaign.Infrastructure;
using Campaign.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Campaign.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Infrastructure
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDB"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Services
            //services.AddMediatR(typeof(CreateProductHandler));
            //services.AddMediatR(typeof(UpdateProductStockHandler));
            //services.AddMediatR(typeof(CreateOrderHandler));
            //services.AddMediatR(typeof(CreateCampaignHandler));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Campaign.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Campaign.API");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
