using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotnetcorepms.Models
{
    public partial class Forums
    {
        [Key]
        public int ID { get; set; }
        public string content { get; set; }
        public bool? created_by_current_user { get; set; }
        public string fullname { get; set; }
        public DateTime? modified { get; set; }
        public int? parent { get; set; }
        public string profile_picture_url { get; set; }
        public int? upvote_count { get; set; }
        public bool? user_has_upvoted { get; set; }
        public DateTime? created { get; set; }
    }
}
