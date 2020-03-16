using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG.DLFinance.SL
{
    public class GeneralService : IGeneralService
    {
        DLFinanceContext db;

        public GeneralService()
        {
            this.db = new DLFinanceContext();
        }

        public GeneralService(DLFinanceContext context)
        {
            this.db = context;
        }


        //get week from a specified date
        public Week GetWeek(bool completed)
        {
            Week week;

      
            bool lastWeek = db.Week.OrderByDescending(x => x.WeekNo).Select(x => x.WeekCompleted).FirstOrDefault();

            if (completed == false && lastWeek == false)
            {
                if (db.Week.Count(x => x.WeekCompleted == true) == 0)
                {
                    week = db.Week.OrderBy(x => x.WeekNo).FirstOrDefault();
                }
                else

                {
                    week = db.Week.OrderBy(x => x.WeekNo).Where(x => x.WeekCompleted == false).FirstOrDefault();
                }
            }
            else
            {
                week = db.Week.OrderByDescending(x => x.WeekNo).Where(x => x.WeekCompleted == true).FirstOrDefault();
            }

            return week;
        }

        public void UpdateWeek(Week week)
        {
            week.WeekCompleted = true;
            db.Entry(week).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        //get all months in the season
        public List<string> GetMonths()
        {
            List<string> monthList = new List<string>();

            List<Week> weekList = db.Week.OrderBy(x => x.WeekNo).ToList();

            if (weekList.Count > 0)
            {
                DateTime startDate = weekList.Where(x => x.WeekNo == 1).Select(p => p.WeekStartDate).FirstOrDefault();

                DateTime endDate = weekList.OrderByDescending(x => x.WeekNo).Select(p => p.WeekEndDate).First();

                while (startDate <= endDate)
                {
                    monthList.Add(startDate.ToString("MMMM"));
                    startDate = startDate.AddMonths(1);
                }
            }
            return monthList;

        }

        //get all weeks from specified month

        public List<int> GetWeekNumbers(DateTime time)
        {
            List<Week> weekList = db.Week.OrderBy(x => x.WeekNo).ToList();

            List<int> weekNoList = weekList.Where(x => x.WeekStartDate.Month == time.Month).Select(p => p.WeekNo).ToList();

            return weekNoList;
        }

        public void ResetWeek(int WeekNo)
        {
            Week week = db.Week.Where(x => x.WeekNo == WeekNo).FirstOrDefault();

            week.WeekCompleted = false;
            db.Entry(week).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }


}