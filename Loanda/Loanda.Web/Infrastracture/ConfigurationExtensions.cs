using Microsoft.Extensions.Configuration;

namespace Loanda.Web.Infrastracture
{ 
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");
    }
}
