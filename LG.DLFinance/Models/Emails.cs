using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{

    [Table("Emails", Schema = "DL")]
    public class Emails
    {

        public Guid EmailsId { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public bool Primary { get; set; }

        public Emails()
        {
            EmailsId = Guid.NewGuid();
        }
    }
}