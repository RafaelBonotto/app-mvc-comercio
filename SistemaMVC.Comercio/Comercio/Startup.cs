using Comercio.Data.ConnectionManager;
using Comercio.Data.Repositories;
using Comercio.Data.Repositories.Produtos;
using Comercio.Data.Repositories.Setores;
using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Interfaces.SetorInterfaces;
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

            // Connection
            services.AddScoped(typeof(IMySqlConnectionManager), typeof(MySqlConnectionManager));
            // Mapper
            services.AddScoped(typeof(IProdutoAdapter), typeof(ProdutoAdapter));
            services.AddScoped(typeof(ISetorAdapter), typeof(SetorAdapter));
            // Services
            services.AddScoped(typeof(IProdutoService), typeof(ProdutoService));
            services.AddScoped(typeof(ISetorService), typeof(SetorService));
            // Repository Base
            services.AddScoped(typeof(IRepositoryBase<Produto>), typeof(ProdutoRepository));
            services.AddScoped(typeof(IRepositoryBase<Setor>), typeof(SetorRepository));
            // Repositorys
            services.AddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));

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
