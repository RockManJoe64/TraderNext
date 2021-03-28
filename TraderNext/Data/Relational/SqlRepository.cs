using System.Linq;
using Microsoft.EntityFrameworkCore;
using TraderNext.Core.Data;

namespace TraderNext.Data.Relational
{
    public abstract class SqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected DbContext DbContext { get; private set; }

        protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

        public IQueryable<TEntity> Query => Set;

        public SqlRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
