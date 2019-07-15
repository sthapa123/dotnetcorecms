using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcorepms.Interfaces
{
    public interface IForum
    {
        IEnumerable<Forums> GetComments();
        IEnumerable<Forums> GetCommentsByID(int id);
        int Commit(Forums model, int mode);
    }
}
