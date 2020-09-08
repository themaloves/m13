using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace M13.InterviewProject
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseIISIntegration()
                .Build();

            await host.RunAsync();
        }
    }
}
