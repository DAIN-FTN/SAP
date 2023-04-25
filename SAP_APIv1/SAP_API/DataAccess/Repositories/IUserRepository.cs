using SAP_API.Models;


namespace SAP_API.DataAccess.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        public User GetByUsername(string username);
    }
}
