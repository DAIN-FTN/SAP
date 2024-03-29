﻿using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _users.Add(entity);
            _context.SaveChanges();

            return GetById(entity.Id);
        }

        public bool Delete(Guid id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _users.Remove(user);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<User> GetAll()
        {
            return _users
                 .Include("Role")
                .ToList();
        }

        public User GetById(Guid id)
        {
            return _users
                .Include("Role")
                .Include(user => user.BakingProgramsMade)
                    .ThenInclude(bp => bp.Oven)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _users
                 .Include("Role")
                .Where(p => p.Username.Equals(username)).FirstOrDefault();
        }

        public List<User> GetUsersByUsername(string name)
        {
            return _users
                 .Include("Role")                
                .Where(u => u.Username.Contains(name)).ToList();
        }

        public User Update(User updatedUser)
        {
            User user = GetById(updatedUser.Id);
            if (user != null)
            {
                _users.Update(updatedUser);
                _context.SaveChanges();

                return GetById(updatedUser.Id);
            }
            throw new Exception("User not found");
        }
    }
}
