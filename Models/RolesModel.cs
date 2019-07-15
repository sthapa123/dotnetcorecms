using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcorepms.Models
{
    public partial class Roles : CommonTwoModel
    {
        [Key]
        public int RoleID { get; set; }
        public string Rolename { get; set; }
    }

    public class RolesViewModel
    {
        public Roles dbModel { get; set; }
    }

    public class RolesViewModelLst
    {
        public IQueryable<Roles> dbModelLst { get; set; }
    }
}
