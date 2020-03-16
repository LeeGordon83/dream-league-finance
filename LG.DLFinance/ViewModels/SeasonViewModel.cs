using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class SeasonViewModel
    {
        [Display(Name = "Week Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime WeekStartDate { get; set; }

        [Display(Name = "Number of Weeks")]
        public int NumberofWeeks { get; set; }

        [Display(Name = "Number of Managers")]
        public int NumberofManagers { get; set; }

    }
}