using System.Linq;

namespace TraderNext.Core.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query { get; }
    }
}
