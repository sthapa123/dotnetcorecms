using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace dotnetcorepms.Models
{
    public partial class Documents
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string file { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? user_id { get; set; }
    }

    public class DoucumentsViewModel
    {
        public Documents dbModel { get; set; }
        public IEnumerable<PairModel> ddlUsers { get; set; }
    }

    public class DocumentsViewModelLst
    {
        public IQueryable<Documents> dbModelLst { get; set; }
        public IEnumerable<PairModel> ddlUser { get; set; }
    }
}
