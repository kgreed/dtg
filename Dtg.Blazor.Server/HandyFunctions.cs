using Microsoft.Extensions.Configuration;
namespace Dtg.Blazor.Server
{
    public class HandyFunctions
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("Connections.json");
            var build = builder.Build();
           var s =build.GetConnectionString("ConnectionString");
           return s;
        }
    }
}