using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WaterMango.Helper;

namespace WaterMango
{
    public class Startup
    { 
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Settings config = new Settings()
            {
                dbJsonPath = System.IO.Path.Combine(_env.ContentRootPath, Configuration["dbJsonPath"])
            };

            services.AddSingleton<Settings, Settings>(a => config);

            //    services.AddDbContext<MyContext>(opt =>
            //          opt.UseInMemoryDatabase("MyList"));


            services.AddControllersWithViews();
             
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddHttpContextAccessor();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "This is my My APIS documentation  ", Version = "v1" });
                 
            });
            services.AddCors(options =>
            { 
                options.AddDefaultPolicy( 
                     o => o.AllowAnyOrigin() 
                    );
            });
             

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(o => o.AllowAnyOrigin());
            app.UseEndpoints(endpoints =>
            {  
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
