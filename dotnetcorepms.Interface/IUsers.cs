using dotnetcorepms.Models;
using System.Linq;

namespace dotnetcorepms.Interfaces
{
    public interface IUsers
    {
        int AddUser(Users entity);
        void AddAdmin(Users entity);
        bool CheckUserNameExists(string Username);
        Users Userinformation(int UserID);
        IQueryable<UsersModel> UserinformationList(string sortColumn, string sortColumnDir, string Search);
        IQueryable<UsersModel> getAllUsers();
        int Commit(Users model, int mode);
    }
}
