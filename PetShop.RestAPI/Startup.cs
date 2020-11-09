using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using PetShop.Core.ApplicationServices;
using PetShop.Core.ApplicationServices.Services;
using PetShop.Core.DomainServices;
using PetShop.Infrastructure.Data;
using PetShop.Infrastructure.Database;
using PetShop.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetShop.Core.Entity;
using PetShop.Infrastructure.Database.Helpers;

namespace PetShop.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });


            services.AddScoped<IUserRepository<TodoItem>, TodoItemRepository>();
            services.AddScoped<IUserRepository<User>, UserRepository>();

            
            services.AddTransient<IDBInitializer, DBInitializer>();

            services.AddSingleton<IAuthenticationHelper>(new
                AuthenticationHelper(secretBytes));

            
            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    })
            );
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder
                    .WithOrigins("https://petshopappangular.web.app/").AllowAnyMethod().AllowAnyHeader()
                    .WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
                    .WithOrigins("https://localhost:44314/api/petshop").AllowAnyMethod().AllowAnyHeader();
            }));

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PetShopContext>(
                    opt => opt.UseInMemoryDatabase("ThaDb"));
            }
            else
            {
                services.AddDbContext<PetShopContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }


            services.AddDbContext<PetShopContext>(opt => opt.UseSqlite("Data Source=petApp.db"));
            services.AddScoped<IPetRepository, PetSqlRepository>();
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IOwnerRepository, OwnerSqlRepository>();
            services.AddScoped<IOwnerService, OwnerService>();

            services.AddScoped<IPetTypeRepository, PetTypeSqlRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddTransient<IDBInitializer, DBInitializer>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v2",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Swagger Demo API",
                        Description = "Demo API for showing Swagger",
                        Version = "v1"
                    });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = scope.ServiceProvider.GetService<PetShopContext>(); 
                    var dbInitializer = services.GetService<IDBInitializer>();
                    dbInitializer.SeedDB(ctx);

                    var petRepo = scope.ServiceProvider.GetService<IPetRepository>();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo API");
            });
        }
    }
}
