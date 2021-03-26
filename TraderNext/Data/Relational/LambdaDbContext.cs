using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TraderNext.Models;

namespace TraderNext.Data.Relational
{
    public class LambdaDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public LambdaDbContext(DbContextOptions<LambdaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetExecutingAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
