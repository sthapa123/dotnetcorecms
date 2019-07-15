using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Interfaces
{
    public interface IDocuments
    {
        IQueryable<Documents> getAllDocuments();
        bool checkDocumentnameExists(string docName);
        int addDocument(Documents entity);
        Documents getDocument(int id);
        int Commit(Documents model, int mode);
    }
}
