
using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<Role> _roles;

        public RoleRepository(BakeryContext context)
        {
            _context = context;
            _roles = context.Set<Role>();
        }
        public Role Create(Role entity)
        {
            _roles.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public bool Delete(Guid id)
        {
            var role = GetById(id);
            if (role != null)
            {
                _roles.Remove(role);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<Role> GetAll()
        {
            return _roles.ToList();
        }

        public Role GetById(Guid id)
        {
            return _roles.FirstOrDefault(r => r.Id == id);
        }

        public Role Update(Role updatedRole)
        {
            Role role = GetById(updatedRole.Id);
            if (role != null)
            {
                _roles.Remove(role);
                _roles.Add(updatedRole);
                _context.SaveChanges();

                return updatedRole;
            }
            throw new Exception("Role not found");
        }
    }
}
