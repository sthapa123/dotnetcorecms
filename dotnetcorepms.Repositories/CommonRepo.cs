using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotnetcorepms.Repositories
{
    public class CommonRepo : ICommon
    {
        private DatabaseContext _context;

        public CommonRepo(DatabaseContext context)
        {
            _context = context;
        }

        public List<PairModel> GetPairModel(string args, long id)
        {
            List<PairModel> _pairModel = new List<PairModel>();

            switch (args)
            {
                case "RoleName":
                    _pairModel = (from c in _context.Roles
                                  select new PairModel { Key = c.RoleID, Value = c.Rolename }).ToList<PairModel>();
                    return _pairModel;
                case "Users":
                    _pairModel = (from c in _context.Users
                                  select new PairModel { Key = c.ID, Value = c.Name }).ToList<PairModel>();
                    return _pairModel;
                case "Notes":
                    _pairModel = (from c in _context.Notes
                                  select new PairModel { Key = c.id, Value = c.note }).ToList<PairModel>();
                    return _pairModel;
                default:
                    return _pairModel;
            }
        }

        public List<PairModel> GetPairModelWithDefault(string args, long id = 0)
        {
            List<PairModel> result = GetPairModel(args, id);
            result.Add(new PairModel { Key = "0", Value = "---Select---" });
            return result;
        }
    }
}
