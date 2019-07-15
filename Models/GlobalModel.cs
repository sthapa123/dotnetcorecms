using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotnetcorepms.Models
{
    public class MessageModel
    {
        public string title { get; set; }
        public string area { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
    }

    public class PairModel
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }

    public class CommonTwoModel
    {
        [Required]
        [Display(Name = "Created On")]
        public DateTime Created_At { get; set; }

        [Required]
        [Display(Name = "Updated On")]
        public DateTime Updated_At { get; set; }
    }
}
