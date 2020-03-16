using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.ViewModels
{
    public class LgCupWinnersLineViewModel
    {

        public LgCupWinnersLineViewModel()
        {
            this.PrizeSelection = new Prizes();
            

        }

        [Display(Name = "League Prize")]
        public Prizes PrizeSelection { get; set; }
        

        [Display(Name = "Manager")]
        public string PickedManager { get; set; }
        public IEnumerable<SelectListItem> ManagerList { get; set; }
    }
}