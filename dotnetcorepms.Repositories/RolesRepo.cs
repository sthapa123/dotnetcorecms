using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcorepms.Repositories
{
    public class RolesRepo : IRoles
    {
        private DatabaseContext _context;

        public RolesRepo(DatabaseContext context)
        {
            _context = context;
        }

        public int getRolesofUserbyRolename(string Rolename)
        {
            var roleID = (from role in _context.Roles
                          where role.Rolename == Rolename
                          select role.RoleID).SingleOrDefault();

            return roleID;
        }

        public IQueryable<Roles> getAllRoles()
        {
            var result = (from c in _context.Roles
                          select new Roles
                          {
                              RoleID = c.RoleID,
                              Rolename = c.Rolename
                          });
            return result;
        }

        public Roles getRole(int id)
        {
            var result = (from role in _context.Roles
                          where role.RoleID == id
                          select new Roles
                          {
                              RoleID = role.RoleID,
                              Rolename = role.Rolename,
                              Created_At = role.Created_At,
                              Updated_At = role.Updated_At
                          }).SingleOrDefault();
            return result;
        }

        public bool checkRolenameExists(string roleName)
        {
            var result = (from c in _context.Roles
                          where c.Rolename.ToLower() == roleName.ToLower()
                          select c).Count();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int addRole(Roles entity)
        {
            _context.Roles.Add(entity);
            return _context.SaveChanges();
        }

        public int Commit(Roles model, int mode)
        {
            int identity = 0;
            // Only persist if any tickets to add or modify.
            if (model == null) return identity;
            // Persist any new or modified tickets.
            if (mode == 0)
            {
                model.Created_At = model.Updated_At = DateTime.Now;
                _context.Roles.Add(model);
                _context.SaveChanges();
                identity = model.RoleID;
            }
            //Update the existing entity
            else if (mode == 1)
            {
                model.Created_At = model.Updated_At = DateTime.Now;
                _context.Roles.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                identity = model.RoleID; ;
            }
            //Remove the existing entity
            else if (mode == 2)
            {
                _context.Roles.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return identity;
        }
    }
}
