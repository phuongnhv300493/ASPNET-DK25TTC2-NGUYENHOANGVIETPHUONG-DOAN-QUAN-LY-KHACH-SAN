using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace datphongkhachsan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
                     // reset database
            /* update-database 00000000000000_CreateIdentitySchema
        remove-migration
        add-migration ver1
        update-database
         update-database*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
