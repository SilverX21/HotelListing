using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //nas próximas linhas estamos a definir as preferências para o serilog
            //path -> define o local físico onde será guardado o txt dos logs, o nome do ficheiro tem um "-" porque no fim é colocada a data dos logs
            //outputTemplate -> é o tipo de mensagem que os logs vão ter:
            //-> {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} será a data com os detalhes todos
            //-> [{Level:u3}] é o tipo de erro que ocorreu
            //-> {Message:lj} a mensagem do erro
            //-> {NewLine} faz uma line break (ou enter)
            //-> {Exception} é a exception que resultou do erro
            //rollingInterval -> aqui digo quando quero criar ficheiros de log novos, neste caso definimos para criar diariamente um ficheiro novo
            //restrictedToMinimumLevel -> aqui definimos o que queremos ver nos logs, aqui definimos para information porque não precisamos de ver tudo
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                path: "C:\\Programação\\HotelListing API\\HotelListing\\Logs\\log-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            try
            {
                Log.Information("Application is starting!"); //aqui criamos um log normal
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start!"); //aqui é um log com informação fatal, ou seja, erros graves
            }
            finally
            {
                Log.CloseAndFlush(); //aqui paramos de criar logs
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() //aqui definimos que vamos utilizar o serilog
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
