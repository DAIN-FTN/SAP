

using SAP_API.Models;

namespace SAP_API.DataAccess.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        public Role GetByName(string name);
    }
}
