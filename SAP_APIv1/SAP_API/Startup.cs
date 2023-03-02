using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAP_API.DataAccess.DbContexts;
using SAP_API.DataAccess.Repositories;
using SAP_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace SAP_API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBakingProgramService, BakingProgramService>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IStockedProductRepository, StockedProductRepository>();
            services.AddSingleton<IBakingProgramRepository, BakingProgramRepository>();
            services.AddSingleton<IOvenRepository, OvenRepository>();
            services.AddTransient<IArrangingProductsToProgramsService, ArrangingProductsToProgramsService>();
            services.AddSingleton<IReservedOrderProductRepository, ReservedOrderProductRepository>();
            services.AddScoped<IStartPreparingService, StartPreparingService>();
            services.AddSingleton<IProductToPrepareRepository, ProductToPrepareRepository>();
            services.AddScoped<IStockedProductService, StockedProductService>();
            services.AddSingleton<IStockLocationRepository, StockLocationRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderTransactionsOrchestrator, OrderTransactionsOrchestrator>();
            services.AddDbContext<BakeryContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        }

    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
