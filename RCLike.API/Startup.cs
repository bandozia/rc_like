using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RCLike.API.Service.Extensions;
using RCLike.Data.Contexts;
using RCLike.Data.Repositories;
using RCLike.Data.Repositories.ef;
using RCLike.Data.Service;

namespace RCLike.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
                
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "RC Like feature", Version = "v2" }));

            services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(Configuration.GetConnectionString("default")));

            services.AddScoped(AuthFactory.JWTTokenService);

            services.AddScoped<ILikerReository, LikerRepository>();
            services.AddScoped<IUrlRepository, UrlRepository>();

            services.AddTransient<ILikeService, LikeService>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RC Like feature API"));
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "static", "dist")),
                RequestPath = "/scripts"
            });

            app.UseRouting();            
            app.UseAuthorization();

            dbContext.Database.EnsureCreated();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
