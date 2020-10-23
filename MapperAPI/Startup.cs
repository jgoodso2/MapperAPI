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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MapperAPI.Entities; 
using MapperAPI.Data;
using MapperAPI.Services; 
using AutoMapper; 


namespace MapperAPI
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
            //addDBContext with sql server
            var foo = Configuration.GetConnectionString("DefaultConnection"); 
            services.AddDbContext<MapperContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging());
            //add controllers
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IProjectInfoRepository, ProjectInfoRepository>();
            //Mapper.AssertConfigurationIsValid();

            services.AddDbContext<MapperAPIContextboo>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MapperAPIContextboo")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            if (1 == 1)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
