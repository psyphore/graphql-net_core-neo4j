using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace net_core_graphql
{
    public class Program
    {
        public static async Task Main(string[] args) => await WebHost.CreateDefaultBuilder<Startup>(args).Build().RunAsync();
    }
}