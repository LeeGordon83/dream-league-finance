using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.ViewModels
{
    public class LgCupWinnersViewModel
    {
        public LgCupWinnersViewModel()
        {
            this.lgCupWinnersLineViewModels = new List<LgCupWinnersLineViewModel>();
        }

        public List<LgCupWinnersLineViewModel> lgCupWinnersLineViewModels { get; set; }

      }
}