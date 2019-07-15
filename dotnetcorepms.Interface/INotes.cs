using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Interfaces
{
    public interface INotes
    {
        IQueryable<Notes> getAllNotes();
        bool checkNotenameExists(string noteName);
        int addNote(Notes entity);
        int Commit(Notes model, int mode);
        Notes getNote(int idNote);
    }
}
