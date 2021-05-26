using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TraderNext.Data.Relational
{
    public class LambdaDbContext : DbContext
    {
        public LambdaDbContext()
        {
        }

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
