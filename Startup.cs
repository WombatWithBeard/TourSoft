using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using ToursSoft.Data.Contexts;

namespace ToursSoft
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>().BuildServiceProvider();  
//            (options => 
//                options.UseNpgsql("Host=localhost;Database=postgres1;Username=postgres;Password=postgres")));
            
            services.AddMvc();

            services.AddSwaggerGen(x => { x.SwaggerDoc("v1", new Info {Title = "Tours API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //TO DO: swagger dont work
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Tour API v1"); });
            app.UseMvc();          
            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });

        }
    }
}