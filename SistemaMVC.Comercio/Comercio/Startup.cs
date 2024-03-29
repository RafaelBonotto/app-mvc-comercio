using Comercio.Data.ConnectionManager;
using Comercio.Data.Repositories.Enderecos;
using Comercio.Data.Repositories.Fornecedores;
using Comercio.Data.Repositories.Produtos;
using Comercio.Data.Repositories.Setores;
using Comercio.Data.Repositories.Telefones;
using Comercio.Data.Repositories.Vendedores;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Interfaces.VendedorInterfaces;
using Comercio.Mapper;
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

            #region Inje��o de Dependencia

            // Connection
            services.AddScoped(typeof(IMySqlConnectionManager), typeof(MySqlConnectionManager));
            // Mapper
            services.AddScoped(typeof(IProdutoAdapter), typeof(ProdutoAdapter));
            services.AddScoped(typeof(ISetorAdapter), typeof(SetorAdapter));
            services.AddScoped(typeof(IFornecedorAdapter), typeof(FornecedorAdapter));
            services.AddScoped(typeof(ITelefoneAdapter), typeof(TelefoneAdapter));
            services.AddScoped(typeof(IEnderecoAdapter), typeof(EnderecoAdapter));
            services.AddScoped(typeof(IVendedorAdapter), typeof(VendedorAdapter));
            // Services
            services.AddScoped(typeof(ISetorService), typeof(SetorService));
            services.AddScoped(typeof(IFornecedorService), typeof(FornecedorService));
            // Repository Base
            services.AddScoped(typeof(IRepositoryBase<Produto>), typeof(ProdutoRepository));
            services.AddScoped(typeof(IRepositoryBase<Setor>), typeof(SetorRepository));
            services.AddScoped(typeof(IRepositoryBase<Fornecedor>), typeof(FornecedorRepository));

            // Repositorys
            services.AddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));
            services.AddScoped(typeof(IFornecedorRepository), typeof(FornecedorRepository));
            services.AddScoped(typeof(ITelefoneRepository), typeof(TelefoneRepository));
            services.AddScoped(typeof(IEnderecoRepository), typeof(EnderecoRepository));
            services.AddScoped(typeof(IVendedorRepository), typeof(VendedorRepository));

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
