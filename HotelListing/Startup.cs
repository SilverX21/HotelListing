using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //aqui vamos inicializar a connection string, onde colocamos o nome da connection string que tá no appsettings.json
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))//.EnableSensitiveDataLogging()
            );

            //aqui utilizaos os serviços para a autenticação
            services.AddAuthentication();
            services.ConfigureIdentity(); //este método está no ServiceExtensions.cs, criado aqui no projeto

            //CORS significa -> Cross Origin Resource Sharing, que define o quão retrito é a partilha de acessos e requests
            //basicamente, se alguém fora da minha empresa, por exemplo, quiser aceder à API, o CORS pode bloquear esse mesmo utilizador
            //em baixo permitimos qualquer pessoa aceder a qualquer método e que mandem qualquer header
            services.AddCors(x =>
            {
                x.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            //configuração do AutoMapper
            services.AddAutoMapper(typeof(MapperInitializer));

            //aqui vamos adicionar a DI para o projeto inteiro
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //aqui em baixo estamos a configurar o swagger (agora vem incluido quando criamos uma API!)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
            });

            //Adicionamos o AddNewtonsoftJson para controlar as coisas referentes à serilization
            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(); //aqui utilizamos o swagger
            //aqui inicializamos o UI, que é aquela página toda bonita com os métodos
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));

            app.UseHttpsRedirection();

            //aqui usamos o Cors
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
