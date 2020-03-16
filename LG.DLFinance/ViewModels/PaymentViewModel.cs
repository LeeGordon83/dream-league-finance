using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.ViewModels
{
    public class PaymentViewModel
    {

        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }

        [Display(Name = "Manager")]
        [Required]
        public string PickedManager { get; set; }
        public IEnumerable<SelectListItem> ManagerList { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public string TransactionType { get; set; }

    }
}