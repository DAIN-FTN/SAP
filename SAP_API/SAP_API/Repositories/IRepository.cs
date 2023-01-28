using SAP_API.Models;

namespace SAP_API.Repositories
{
    public interface IRepository<T, TId> where T : IEntity, TId: Guid
    {
        IEnumerable<T> getAll();
        IEnumerable<T> getBy(Guid id);
        T create(T entity);
        T update(T entity);
        T delete(Guid id);
    }
}
