using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Store.NourhanRageb.APIs.Errors;
using Store.NourhanRageb.Domain.Mapping.Products;
using Store.NourhanRageb.Domain.Services.Contract;
using Store.NourhanRageb.Domain;
using Store.NourhanRageb.Repository.Data.Contexts;
using Store.NourhanRageb.Repository;
using Store.NourhanRageb.Service.Services.Products;
using Store.NourhanRageb.Domain.Repositories.Contract;
using Store.NourhanRageb.Repository.Repositories;
using StackExchange.Redis;
using Store.NourhanRageb.Domain.Mapping.Baskets;

namespace Store.NourhanRageb.APIs.Helper
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefiendService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
        private static IServiceCollection AddUserDefiendService(this IServiceCollection services)
        {
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));

            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConnectionMultiplexer>((servicesProvider) =>
            {
               var connection =  configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            return services;
        }
    }
}
