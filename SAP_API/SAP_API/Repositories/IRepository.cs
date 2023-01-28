using SAP_API.Models;

namespace SAP_API.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        bool Delete(Guid id);
    }
}
