using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.WebApi.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabases(this WebApplicationBuilder builder)
        {
            //PostgreSQL
            builder.Services.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );
            //Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!)
            );
            //MongoDB
            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
            builder.Services.AddScoped<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase("SalesDb"));
        }
    }
}