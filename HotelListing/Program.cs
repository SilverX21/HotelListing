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
            //nas pr�ximas linhas estamos a definir as prefer�ncias para o serilog
            //path -> define o local f�sico onde ser� guardado o txt dos logs, o nome do ficheiro tem um "-" porque no fim � colocada a data dos logs
            //outputTemplate -> � o tipo de mensagem que os logs v�o ter:
            //-> {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} ser� a data com os detalhes todos
            //-> [{Level:u3}] � o tipo de erro que ocorreu
            //-> {Message:lj} a mensagem do erro
            //-> {NewLine} faz uma line break (ou enter)
            //-> {Exception} � a exception que resultou do erro
            //rollingInterval -> aqui digo quando quero criar ficheiros de log novos, neste caso definimos para criar diariamente um ficheiro novo
            //restrictedToMinimumLevel -> aqui definimos o que queremos ver nos logs, aqui definimos para information porque n�o precisamos de ver tudo
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                path: "C:\\Programa��o\\HotelListing API\\HotelListing\\Logs\\log-.txt",
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
                Log.Fatal(ex, "Application failed to start!"); //aqui � um log com informa��o fatal, ou seja, erros graves
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
