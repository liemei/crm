using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using bnuxq.Common;

namespace bnuxq.crm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            ClassLoger.StartLogin();
            clearlogs clearlog = new clearlogs();
            clearlog.Start();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
