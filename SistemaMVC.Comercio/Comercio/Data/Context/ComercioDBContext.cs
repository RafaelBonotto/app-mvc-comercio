using Microsoft.EntityFrameworkCore;

namespace Sistema.Comercio.Data.Context
{
    public class ComercioDBContext : DbContext
    {
        public ComercioDBContext(DbContextOptions<ComercioDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComercioDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

