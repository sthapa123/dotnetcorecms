using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Repositories
{
    public class NotesRepo : INotes
    {
        private DatabaseContext _context;

        public NotesRepo(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Notes> getAllNotes()
        {
            var result = (from c in _context.Notes
                          select new Notes
                          {
                              id = c.id,
                              note = c.note,
                              created_at = c.created_at,
                              updated_at = c.updated_at,
                              user_id = c.user_id
                          });
            return result;
        }

        public bool checkNotenameExists(string noteName)
        {
            var result = (from c in _context.Notes
                          where c.note.ToLower().ToString() == noteName.ToLower().ToString()
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

        public Notes getNote(int idNote)
        {
            var result = (from note in _context.Notes
                          where note.id == idNote
                          select new Notes
                          {
                              id = note.id,
                              created_at= note.created_at,
                              updated_at = note.updated_at,
                              note = note.note,
                              user_id = note.user_id,
                          }).SingleOrDefault();
            return result;
        }

        public int addNote(Notes entity)
        {
            _context.Notes.Add(entity);
            return _context.SaveChanges();
        }

        public int Commit(Notes model, int mode)
        {
            int identity = 0;
            // Only persist if any tickets to add or modify.
            if (model == null) return identity;
            // Persist any new or modified tickets.
            if (mode == 0)
            {
                model.created_at = model.updated_at = DateTime.Now;
                _context.Notes.Add(model);
                _context.SaveChanges();
                identity = model.id;
            }
            //Update the existing entity
            else if (mode == 1)
            {
                model.created_at = model.updated_at = DateTime.Now;
                _context.Notes.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                identity = model.id; ;
            }
            //Remove the existing entity
            else if (mode == 2)
            {
                _context.Notes.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return identity;
        }
    }
}
