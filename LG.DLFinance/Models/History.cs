using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{

    [Table("History", Schema = "DL")]
    public class History
    {
        [Key]
        public Guid historyId { get; set; }

        [Display(Name = "Season")]
        public int year { get; set; }

        [Display(Name = "No. of Managers")]
        public int numberOfManagers { get; set; }

        [Display(Name = "Most Money Overall")]
        public string mostMoney { get; set; }

        [Display(Name = "Most Fivers")]
        public string mostFivers { get; set; }

        [Display(Name = "Least Fivers")]
        public string leastFivers { get; set; }

        [Display(Name = "Most Weekly Prizes")]
        public string mostWeekly { get; set; }

        [Display(Name = "Least Weekly Prizes")]
        public string leastWeekly { get; set; }

        [Display(Name = "Biggest Jackpot Won")]
        public string biggestJackpot { get; set; }
    }
}