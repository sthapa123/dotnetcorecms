using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace dotnetcorepms.Repositories
{
    public class UsersRepo : IUsers
    {
        private DatabaseContext _context;

        public UsersRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddAdmin(Users entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public int AddUser(Users entity)
        {
            _context.Users.Add(entity);
            return _context.SaveChanges();
        }

        public bool CheckUserNameExists(string Username)
        {
            var result = (from user in _context.Users
                          where user.Username == Username
                          select user).Count();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Users Userinformation(int UserID)
        {
            var result = (from user in _context.Users
                          where user.ID == UserID
                          select new Users
                          { 
                            Name =user.Name,
                            EmailID =user.EmailID,
                            CreatedOn = user.CreatedOn.Value,
                            Username = user.Username,
                            Password = user.Password,
                            ConfirmPassword = user.ConfirmPassword,
                            ID = user.ID,
                            RoleID = user.RoleID
                          }).SingleOrDefault();
            return result;
        }

        public IQueryable<UsersModel> UserinformationList(string sortColumn, string sortColumnDir, string Search)
        {
            var IQueryableReg = (from user in _context.Users
                                 select new UsersModel
                                 {
                                     Name = user.Name,
                                     EmailID = user.EmailID,
                                     CreatedOn = user.CreatedOn.Value.ToString("dd/MM/yyyy"),
                                     Username = user.Username
                                 });
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
               // IQueryableReg = IQueryableReg.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableReg = IQueryableReg.Where(m => m.Username == Search || m.EmailID == Search);
            }

            return IQueryableReg;
        }

        public IQueryable<UsersModel> getAllUsers()
        {
            var user = (from c in _context.Users
                         select new UsersModel
                         {
                             ID = c.ID,
                             Name = c.Name,
                             EmailID = c.EmailID,
                             RoleID = c.RoleID
                         });
            return user;
        }
        public int Commit(Users model, int mode)
        {
            int identity = 0;
            // Only persist if any tickets to add or modify.
            if (model == null) return identity;
            // Persist any new or modified tickets.
            if (mode == 0)// Create
            {
                model.CreatedOn = DateTime.Now;
                _context.Users.Add(model);
                _context.SaveChanges();
                identity = model.ID;
            }
            //Update the existing entity
            else if (mode == 1)
            {
                model.CreatedOn = DateTime.Now;
                _context.Users.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                identity = model.ID; ;
            }
            //Remove the existing entity
            else if (mode == 2)
            {
                _context.Users.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return identity;
        }
    }
}
