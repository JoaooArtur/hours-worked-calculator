using DependencyInjection.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DependencyInjection.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        public static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api/swagger";

                foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                    options.SwaggerEndpoint($"/api/swagger/{version}/swagger.json", version);

                options.DisplayRequestDuration();
                options.EnableTryItOutByDefault();
                options.DocExpansion(DocExpansion.None);
            });
        }
    }
}
