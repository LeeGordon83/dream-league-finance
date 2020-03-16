using System;
using System.Collections.Generic;
using LG.DLFinance.Models;

namespace LG.DLFinance.SL
{
    public interface IGeneralService
    {
        Week GetWeek(bool completed);

        List<string> GetMonths();

        void UpdateWeek(Week week);

        List<int> GetWeekNumbers(DateTime time);

        void ResetWeek(int WeekNo);
    }
}