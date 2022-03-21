using DevAttic.ConfigCrypter.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((hostingEnv, config) =>
            {
                config.AddEncryptedAppSettings(crypter =>
                {
                    crypter.CertificatePath = "MyApp.pfx";
                    crypter.KeysToDecrypt = new List<string>
                    {
                        "ConnectionStrings:DefaultConnection",
                        "SensitiveInfo:RandomKey"
                    };
                });
            });
    }
}
