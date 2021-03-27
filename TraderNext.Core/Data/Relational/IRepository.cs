using System.Linq;

namespace TraderNext.Data.Relational
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query { get; }
    }
}
