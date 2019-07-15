using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcorepms.Interfaces
{
    public interface IRoles
    {
        int getRolesofUserbyRolename(string Rolename);
        IQueryable<Roles> getAllRoles();
        bool checkRolenameExists(string roleName);
        int addRole(Roles entity);
        Roles getRole(int id);
        int Commit(Roles model, int mode);
    }
}
