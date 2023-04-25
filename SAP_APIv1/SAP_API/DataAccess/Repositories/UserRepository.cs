using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(BakeryContext context)
        {
            _context = context;
            _users = context.Set<User>();
        }
        public User Create(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
