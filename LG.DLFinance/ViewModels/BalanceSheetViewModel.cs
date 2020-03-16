using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class BalanceSheetViewModel
    {
        [Display(Name = "Weekly Fees")]
        public decimal WeeklyFees { get; set; }

        [Display(Name = "Joining Fees")]
        public decimal JoiningFees { get; set; }
        [Display(Name = "Cup Entry")]
        public decimal CupEntry { get; set; }
        [Display(Name = "League Cup Entry")]
        public decimal LeagueCupEntry { get; set; }

        [Display(Name = "Jackpot Carry Over")]
        public decimal JackpotCarryOver { get; set; }
        [Display(Name = "Fiver Draws")]
        public decimal FiverDraws { get; set; }

        [Display(Name = "Fiver Draws Paid")]
        public decimal FiverDrawsOut { get; set; }

        public decimal Jackpot { get; set; }

        [Display(Name = "Jackpot Paid")]
        public decimal JackpotOut { get; set; }

        [Display(Name = "Weekly Prize")]
        public decimal WeeklyPrize { get; set; }

        [Display(Name = "Weekly Prize Paid")]
        public decimal WeeklyPrizeOut { get; set; }
        [Display(Name = "End of Season Prizes")]
        public decimal EndOfSeason { get; set; }

        [Display(Name = "End of Season Prizes Paid")]
        public decimal EndOfSeasonOut { get; set; }

        [Display(Name = "Total In")]
        public decimal TotalIn { get; set; }
        [Display(Name = "Total Out")]
        public decimal TotalOut { get; set; }

        [Display(Name = "Current Total In")]
        public decimal CurrentTotalIn { get; set; }

        [Display(Name = "Current Total Out")]
        public decimal CurrentTotalOut { get; set; }

    }
}