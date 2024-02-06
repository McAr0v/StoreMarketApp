
using Autofac;
using Autofac.Extensions.DependencyInjection;
using StoreMarketApp.Abstractions;
using StoreMarketApp.Contexts;
using StoreMarketApp.Mapper;
using StoreMarketApp.Models;
using StoreMarketApp.Services;

namespace StoreMarketApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StoreContext>();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterType<ProductServices>().As<IProductServices>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache(x=>x.TrackStatistics=true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}