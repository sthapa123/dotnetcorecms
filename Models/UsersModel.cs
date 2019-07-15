using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace dotnetcorepms.Models
{
    public partial class Users
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string EmailID { get; set; }

        [MinLength(6, ErrorMessage = "Minimum Username must be 6 in charaters")]
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        [MinLength(7, ErrorMessage = "Minimum Password must be 7 in charaters")]
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Enter Valid Password")]
        public string ConfirmPassword { get; set; }

        public int? RoleID { get; set; }

        public DateTime? CreatedOn { get; set; }  
    }

    public class UsersModel 
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public int? RoleID { get; set; } 
        public string CreatedOn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UsersViewModel
    {
        public Users dbModel { get; set; }
        public IEnumerable<PairModel> ddlRoleLst { get; set; }
    }

    public class UsersViewModelLst
    {
        public IQueryable<UsersModel> dbModelLst { get; set; }
        public List<PairModel> ddlRoleLst { get; set; }
        public string sessionUsername { get; set; }
    }
}
