 using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<BakeryContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockedProductRepository, StockedProductRepository>();
            services.AddScoped<IOvenRepository, OvenRepository>();
            services.AddScoped<IReservedOrderProductRepository, ReservedOrderProductRepository>();
            services.AddScoped<IBakingProgramRepository, BakingProgramRepository>();
            services.AddScoped<IProductToPrepareRepository, ProductToPrepareRepository>();
            services.AddScoped<IStockLocationRepository, StockLocationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBakingProgramService, BakingProgramService>();
            services.AddScoped<IArrangingProductsToProgramsService, ArrangingProductsToProgramsService>();
            services.AddScoped<IStartPreparingService, StartPreparingService>();
            services.AddScoped<IStockedProductService, StockedProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockLocationService, StockLocationService>();
            services.AddScoped<IOrderTransactionsOrchestrator, OrderTransactionsOrchestrator>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(MyAllowSpecificOrigins);

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
