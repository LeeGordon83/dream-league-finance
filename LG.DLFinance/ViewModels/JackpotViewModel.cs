using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class JackpotViewModel
    {
        public List<Transaction> transactions { get; set; }

        public List<Jackpot> jackpots { get; set; }
    }
}