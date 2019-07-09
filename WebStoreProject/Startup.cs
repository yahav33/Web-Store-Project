using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStoreProject.Data;
using WebStoreProject.Services;

namespace WebStoreProject
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("MyConnection");
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString);

            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });
           
            services.AddMvc();
            services.AddScoped<IRepositoryProducts, RepositoryProducts>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddTransient<ICheckTime, CheckTime>();
            services.AddTransient<IReadPhoto, ReadPhoto>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IReadFromBrowser, ReadForBrowser>();
            services.AddTransient<IWriteToBrowser, WriteToBrowser>();
            services.AddTransient<IEmptyCart, EmptyCart>();
            services.AddTransient<ICounterCart, CouterCart>();
            services.AddTransient<IEmailManger, EmailManger>();
            services.AddTransient<ICheckUserExist, CheckUserExist>();
            services.AddTransient<ILogger, Logger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,StoreContext storeContext)
        {
            storeContext.Database.EnsureCreated();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = "" }
                );
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Something went wrong!");
            });
        }
    }
}
