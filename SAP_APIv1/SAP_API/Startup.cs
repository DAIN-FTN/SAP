using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAP_API.Repositories;
using SAP_API.Services;

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
