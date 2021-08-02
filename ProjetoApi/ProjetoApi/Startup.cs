using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoApi.Models;

namespace ProjetoApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string ContentRoot { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ContentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var conexaoBd = Configuration.GetConnectionString("ContextoBd");
            services.AddDbContext<ContextoBd>(options => options.UseSqlServer(conexaoBd));

            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration["Securitykey"]);

            services.AddAuthentication(n =>
            {
                n.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                n.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(n =>
            {
                n.RequireHttpsMetadata = false;
                n.SaveToken = true;
                n.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(n =>
            {
                n.SwaggerDoc("v1", new OpenApiInfo { Title = "Maykell - GlobalTec Api", Version = "v1" });

                n.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor inseirir Jwt com Bearer no campo",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                n.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // Defina o caminho dos comentários para o Swagger JSON e a interface do usuário.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                n.IncludeXmlComments(xmlPath);

            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ContextoBd>();
                InicializaBd.Inicializar(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(n =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(n.RoutePrefix) ? "." : "..";
                n.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Maykell - GlobalTec Api V1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(n => n.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
