using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM;

/// <summary>
/// Apply migrations automatically
/// </summary>
[ExcludeFromCodeCoverage]
public static class MigrationConfig
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DefaultContext>();
        dataContext.Database.Migrate();
    }
}