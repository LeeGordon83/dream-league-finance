using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{

    [Table("Week", Schema = "DL")]
    public class Week
    {
        [Key]
        [Display(Name = "Week Number")]
        public int WeekNo { get; set; }

        [Display(Name = "Week Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime WeekStartDate { get; set; }

        [Display(Name = "Week End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime WeekEndDate { get; set; }

        [Display(Name = "Week Completed")]
        public bool WeekCompleted { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

    }
}