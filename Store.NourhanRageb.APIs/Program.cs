
using Microsoft.EntityFrameworkCore;
using Store.NourhanRageb.Domain.Mapping.Products;
using Store.NourhanRageb.Domain.Services.Contract;
using Store.NourhanRageb.Domain;
using Store.NourhanRageb.Repository.Data.Contexts;
using Store.NourhanRageb.Repository;
using Store.NourhanRageb.Service.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Store.NourhanRageb.APIs.Errors;
using Store.NourhanRageb.APIs.Middlewares;
using Store.NourhanRageb.APIs.Helper;

namespace Store.NourhanRageb.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

           await app.ConfigurateMiddlewaresAsync();
            
            app.Run();
        }
    }
}
