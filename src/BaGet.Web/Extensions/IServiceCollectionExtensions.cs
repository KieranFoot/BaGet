using System;
using BaGet.Core;
using BaGet.Web;
using Microsoft.Extensions.DependencyInjection;

namespace BaGet
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBaGetWebApplication(
            this IServiceCollection services,
            Action<BaGetApplication> configureAction)
        {
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .AddApplicationPart(typeof(PackageContentController).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.AddTransient<IUrlGenerator, BaGetUrlGenerator>();

            services.AddBaGetApplication(configureAction);

            return services;
        }
    }
}
