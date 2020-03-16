using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class SmallPrizeViewModel
    {
        [Key]
        public Guid ManagerId { get; set; }
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }
        public bool Checkbox { get; set; }
    }
}