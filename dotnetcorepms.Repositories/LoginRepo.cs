using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Linq;

namespace dotnetcorepms.Repositories
{
    public class LoginRepo : ILogin
    {
        private DatabaseContext _context;

        public LoginRepo(DatabaseContext context)
        {
            _context = context;
        }

        public Users ValidateUser(string email, string passWord)
        {
            try
            {
                var validate = (from c in _context.Users
                                where c.EmailID == email && c.Password == passWord
                                select c).SingleOrDefault();

                return validate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdatePassword(Users Registration)
        {
            _context.Users.Attach(Registration);
            _context.Entry(Registration).Property(x => x.Password).IsModified = true;
            int result = _context.SaveChanges();

            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
