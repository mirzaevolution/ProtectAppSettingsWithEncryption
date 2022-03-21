using DevAttic.ConfigCrypter.CertificateLoaders;
using DevAttic.ConfigCrypter.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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
                    crypter.CertificatePath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.pfx");
                    //crypter.CertificateLoader = new CertificateLoader();
                    crypter.KeysToDecrypt = new List<string>
                    {
                        "ConnectionStrings:DefaultConnection",
                        "SensitiveInfo:RandomKey"
                    };
                });
            });
    }

    public class CertificateLoader : ICertificateLoader
    {
        public X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2("MyApp.pfx", string.Empty, X509KeyStorageFlags.MachineKeySet);
        }
    }
}
