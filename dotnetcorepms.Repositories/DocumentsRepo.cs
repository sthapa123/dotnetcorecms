using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Repositories
{
    public class DocumentsRepo : IDocuments
    {
        private DatabaseContext _context;

        public DocumentsRepo(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Documents> getAllDocuments()
        {
            var result = (from c in _context.Documents
                          select new Documents
                          {
                              id = c.id,
                              name = c.name,
                              description = c.description,
                              file = c.file,
                              created_at = c.created_at,
                              updated_at = c.updated_at,
                              user_id = c.user_id
                          });
            return result;
        }

        public Documents getDocument(int id)
        {
            var result = (from document in _context.Documents
                          where document.id == id
                          select new Documents
                          {
                              created_at = document.created_at,
                              description = document.description,
                              file = document.file,
                              id = document.id,  
                              user_id = document.user_id,
                              name = document.name,
                              updated_at = document.updated_at
                          }).SingleOrDefault();
            return result;
        }

        public bool checkDocumentnameExists(string docName)
        {
            var result = (from c in _context.Documents
                          where c.name.ToLower() == docName.ToLower()
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

        public int addDocument(Documents entity)
        {
            _context.Documents.Add(entity);
            return _context.SaveChanges();
        }

        public int Commit(Documents model, int mode)
        {
            int identity = 0;
            // Only persist if any tickets to add or modify.
            if (model == null) return identity;
            // Persist any new or modified tickets.
            if (mode == 0)
            {
                model.created_at = model.updated_at = DateTime.Now;
                _context.Documents.Add(model);
                _context.SaveChanges();
                identity = model.id;
            }
            //Update the existing entity
            else if (mode == 1)
            {
                model.created_at = model.updated_at = DateTime.Now;
                _context.Documents.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                identity = model.id; ;
            }
            //Remove the existing entity
            else if (mode == 2)
            {
                _context.Documents.Attach(model);
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            return identity;
        }
    }
}
