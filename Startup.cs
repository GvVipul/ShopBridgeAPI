
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShowBridge.Services.ProductService;
using ShowBridge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ShowBridge.Services.TokenService;
using System;

namespace ShowBridge
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
            string role = "";
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShowBridge", Version = "v1" });
                c.AddSecurityDefinition("X-BMM-ClientSecret",
                       new OpenApiSecurityScheme()
                       {
                           Name = "X-BMM-ClientSecret",
                           Type = SecuritySchemeType.ApiKey,
                           In = ParameterLocation.Header,
                           Description = "Enter Client Secret ID."
                       }
                   );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "X-BMM-ClientSecret"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IAPITokenValidationService, APITokenValidationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShowBridge v1"));
            }

            app.UseAuthentication();//adding middleware
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
