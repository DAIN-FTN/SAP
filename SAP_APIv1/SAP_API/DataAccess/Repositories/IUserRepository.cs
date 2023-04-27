using SAP_API.Models;
using System.Collections.Generic;

namespace SAP_API.DataAccess.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        public User GetByUsername(string username);
        List<User> GetUsersByUsername(string name);
    }
}
