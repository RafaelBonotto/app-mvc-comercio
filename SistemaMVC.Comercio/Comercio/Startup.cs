using Comercio.Data.ConnectionManager;
using Comercio.Data.Repositories;
using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Mapper;
using Comercio.Models;
using Comercio.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Comercio
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
            services.AddControllersWithViews();            
            services.AddControllers();
            services.AddCors();

            #region Injeção de Dependencia
            services.AddScoped(typeof(IMySqlConnectionManager), typeof(MySqlConnectionManager));
            services.AddScoped(typeof(IRepositoryBase<Produto>), typeof(ProdutoRepository));
            services.AddScoped(typeof(IProdutoAdapter), typeof(ProdutoAdapter));
            services.AddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));
            services.AddScoped(typeof(IProdutoService), typeof(ProdutoService));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
