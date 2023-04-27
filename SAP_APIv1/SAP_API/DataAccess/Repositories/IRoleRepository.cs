

using SAP_API.Models;
using System.Collections.Generic;

namespace SAP_API.DataAccess.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        Role GetByName(string name);
    }
}
