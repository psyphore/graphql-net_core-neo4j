using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace net_core_graphql
{
    public class Program
    {
        public static Task Main(string[] args) => WebHost.CreateDefaultBuilder<Startup>(args).Build().RunAsync();
    }
}