using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG.DLFinance.ViewModels
{
    public class EmailViewModel
    {
        public List<Manager> ManagerList { get; set; }

        public String CurrentMonth { get; set; }

        public Week CurrentWeek { get; set; }

        public Jackpot currentJackpot { get; set; }

        public List<Transaction> wonJackpot { get; set; }

        public List<Transaction> monthTransactions { get; set; }

        public List<WeeklyWinnerSummaryViewModel> weeklyWinners { get; set; }

        public Manager Manager { get; set; }
    }
}