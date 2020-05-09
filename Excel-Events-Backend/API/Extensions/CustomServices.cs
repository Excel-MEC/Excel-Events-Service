using API.Services;
using API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class CustomServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Add Google Cloud Storage
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();

            // Add Event Service
            services.AddScoped<IEventService, EventService>();

            // Add Highlight Service
            services.AddScoped<IHighlightService, HighlightService>();
        }
    }
}