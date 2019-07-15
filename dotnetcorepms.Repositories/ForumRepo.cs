using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Repositories
{
    public class ForumRepo : IForum
    {
        private DatabaseContext _context;

        public ForumRepo(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Forums> GetComments()
        {
            return (from c in _context.Forums
                    select new Forums
                    {
                        content = c.content,
                        created = c.created,
                        //created_by_admin = c.created_by_admin,
                        created_by_current_user = c.created_by_current_user,
                        fullname = c.fullname,
                        ID = c.ID,
                        modified = c.modified,
                        parent = c.parent,
                        profile_picture_url = c.profile_picture_url,
                        upvote_count = c.upvote_count,
                        user_has_upvoted = c.user_has_upvoted,
                    }).ToList();
        }

        public IEnumerable<Forums> GetCommentsByID(int id)
        {
            return (from c in _context.Forums
                    where c.ID == id
                    select new Forums
                    {
                        content = c.content,
                        created = c.created,
                        // created_by_admin = c.created_by_admin,
                        created_by_current_user = c.created_by_current_user,
                        fullname = c.fullname,
                        ID = c.ID,
                        modified = c.modified,
                        parent = c.parent,
                        profile_picture_url = c.profile_picture_url,
                        upvote_count = c.upvote_count,
                        user_has_upvoted = c.user_has_upvoted,
                    }).ToList();
        }

        public int Commit(Forums model, int mode)
        {
            int identity = 0;
            // Only persist if any tickets to add or modify.
            if (model == null) return identity;
                // Persist any new or modified tickets.
                if (mode == 0)
                {
                    model.created = model.modified = DateTime.Now;
                    _context.Forums.Add(model);
                    _context.SaveChanges();
                    identity = model.ID;
                }
                //Update the existing entity
                else if (mode == 1)
                {
                    model.created = model.modified = DateTime.Now;
                    _context.Forums.Attach(model);
                    _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    identity = model.ID; ;
                }
                //Remove the existing entity
                else if (mode == 2)
                {
                    _context.Forums.Attach(model);
                    _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _context.SaveChanges();
                }
            return identity;
        }
    }
}
