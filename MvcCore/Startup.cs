using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PathProvider>();
            String cadenasql = this.Configuration.GetConnectionString("cadenasqlcoches");
            String cadenamysql = this.Configuration.GetConnectionString("cadenamysqlcoches");
            services.AddTransient<RepositoryCochesSQL>();
            services.AddTransient<RepositoryCochesXML>();
            services.AddTransient<RepositoryCochesMySQL>();
            services.AddDbContext<CochesContext>(options => options.UseSqlServer(cadenasql));
            //services.AddDbContextPool<CochesContext>(options => options.UseMySql(cadenamysql, ServerVersion.AutoDetect(cadenamysql))
            //);

            services.AddTransient<IRepositoryCoches, RepositoryCochesSQL>();
            //services.AddTransient<IRepositoryCoches, RepositoryCochesXML>();
            //services.AddTransient<IRepositoryCoches, RepositoryCochesMySQL>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
