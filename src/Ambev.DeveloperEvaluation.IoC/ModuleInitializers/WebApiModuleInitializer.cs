using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    [ExcludeFromCodeCoverage]
    public class WebApiModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks();
            builder.AddBasicHealthChecks();
        }
    }
}