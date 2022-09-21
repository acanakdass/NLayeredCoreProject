using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Repository.Contexts.EntityBuilders;

namespace Repository.Contexts
{
    public class BaseDbContext : DbContext
    {
        private IConfiguration Configuration { get; set; }
        public DbSet<Product> Products { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductModelBuilder.Build(modelBuilder);
            ProductModelBuilder.Seed(modelBuilder);
        }
    }
}