using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var con = "Host=localhost;Port=5432;Database=tours;Username=postgres;Password=postgres";
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options => 
                { options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            
            services.AddSwaggerGen(x => { x.SwaggerDoc("v1", new Info {Title = "Tours API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            
            //TO DO: swagger dont work
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/v1/swagger.json", "Tour API v1"); });
            
            app.UseMvc();        
//            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });

        }
    }
}