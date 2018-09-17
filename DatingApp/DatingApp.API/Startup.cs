using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DatingApp.API
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
            services.AddDbContext<DataContext>(x=> x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //config cors for all domains
            //services.AddCors();

            //config cors
            //services.AddCors(opt => opt.AddPolicy("AllowSpecificOrigin", b => b.WithOrigins("http://localhost:4200/")));
            services.AddCors(opt => opt.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            //criando o repositório global
            services.AddScoped<IAuthRepository, AuthRepository>();
        
            //add authentication service
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            //config cors for all
            //app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //config cors apenas para um dominio
            app.UseCors("AllowSpecificOrigin");

            app.Use(async (context, next) =>
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();
                if (authHeader != null && authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                {   
                    var tokenStr = authHeader.Substring("Bearer ".Length).Trim();
                    System.Console.WriteLine(tokenStr);
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(tokenStr) as JwtSecurityToken;
                    var nameid = token.Claims.First(claim => claim.Type == "nameid").Value;

                    var identity = new ClaimsIdentity(token.Claims);
                    context.User = new ClaimsPrincipal(identity);
                }
                await next();
            });

            //add authentication --sempre chamar antes do UseMVC
            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseMvc(); //Este use SEMPRE TEM QUE SER O ÚLTIMO
        }
    }
}
