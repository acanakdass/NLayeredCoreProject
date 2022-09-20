using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

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
            modelBuilder.Entity<Product>(a =>
            {
                a.ToTable("Products").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.Desc).HasColumnName("Desc");
            });
            // modelBuilder.Entity<Tech>(x =>
            // {
            //     x.ToTable("Techs").HasKey(k => k.Id);
            //     x.Property(p => p.Id).HasColumnName("Id");
            //     x.Property(p => p.Name).HasColumnName("Name");
            //     x.Property(p => p.LangId).HasColumnName("TechId");
            //     x.HasOne(t => t.Lang);
            // });


            Product[] productEntitySeeds =
            {
                new() {Id = 1, Name = "Pen",Desc="Pen Description"},
                new() {Id = 2, Name = "Pencil",Desc="Pen Description"}
            };
            modelBuilder.Entity<Product>().HasData(productEntitySeeds);

            // Tech[] techEntitySeeds =
            // {
            //     new() {Id = 1, Name = ".NET", LangId = 1},
            //     new() {Id = 2, Name = "Spring Boot", LangId = 2}
            // };
            // modelBuilder.Entity<Tech>().HasData(techEntitySeeds);
            
        }
    }
}