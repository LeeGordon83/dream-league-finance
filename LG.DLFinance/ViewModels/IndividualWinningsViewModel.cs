using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class IndividualWinningsViewModel
    {
        public decimal Fivers { get; set; }

        public decimal Weekly { get; set; }

        public decimal Jackpot { get; set; }

        public decimal League { get; set; }

        public decimal Cup { get; set; }

        public decimal Total { get; set; }

        public decimal Payout { get; set; }
    }
}