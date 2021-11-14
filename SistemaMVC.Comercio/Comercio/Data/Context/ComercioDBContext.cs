using Comercio.Models;
using Microsoft.EntityFrameworkCore;

namespace Comercio.Data.Context
{
    public class ComercioDBContext : DbContext
    {
        public ComercioDBContext(DbContextOptions<ComercioDBContext> options) : base(options) { }

        public DbSet<Produto> TB_PRODUTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produto");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComercioDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

