using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    [Table("Fees", Schema = "DL")]
    public class Fees
    {

        public Guid FeesId { get; set; }
        [Display(Name = "Fee Type")]
        public string FeeType { get; set; }
        [Display(Name = "Fee Amount")]
        public decimal FeeAmount { get; set; }
        
        public Fees()
        {
            FeesId = Guid.NewGuid();
        }
    }
}