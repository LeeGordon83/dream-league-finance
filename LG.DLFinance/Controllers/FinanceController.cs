using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.SL;
using LG.DLFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.Controllers
{
    public class FinanceController : Controller
    {

        DLFinanceContext db;

        IFinanceService FinanceService;
        IGeneralService GeneralService;

        public FinanceController()
        {
            this.db = new DLFinanceContext();
            this.FinanceService = new FinanceService(db, new GeneralService());
            this.GeneralService = new GeneralService(db);
        }

        public FinanceController(DLFinanceContext context, IFinanceService financeService, IGeneralService generalService)
        {
            this.db = context;
            this.FinanceService = financeService;
            this.GeneralService = generalService;
        }


        //paid in view
        public ActionResult PaidIn()
        {
            PaidInViewModel paidInView = FinanceService.PaidIn();

            return View(paidInView);
        }

        //paid in view partials
        public ActionResult _PaidInLine(Guid Manager)
        {
            PaidInLineViewModel newLine = FinanceService.PaidInLine(Manager);

            return PartialView(newLine);
        }

        public ActionResult BalanceOverview()
        {
            BalanceSheetViewModel balanceVM = FinanceService.BalanceOverview();

            return View(balanceVM);
        }

        public ActionResult CreditOverview()
        {
            PaidInViewModel paidInView = FinanceService.PaidIn();

            return View(paidInView);
        }

        //credit view partials
        public ActionResult _CreditLine(Guid Manager)
        {
            bool owed = false;

            CreditLineViewModel newLine = FinanceService.CreditLine(Manager, owed);

            return PartialView(newLine);
        }

        public ActionResult _OwedLine()
        {
            bool owed = true;
            Guid Manager = Guid.NewGuid();

            CreditLineViewModel newLine = FinanceService.CreditLine(Manager, owed);

            return PartialView(newLine);
        }

        //credit view partials
        public ActionResult _CreditLineEmail(Guid Manager)
        {
            bool owed = false;

            CreditLineViewModel newLine = FinanceService.CreditLine(Manager, owed);

            return PartialView(newLine);
        }

        //credit view partials
        public ActionResult _CreditLineEmailMonth(Guid Manager)
        {
            bool owed = false;

            DateTime currentDate = DateTime.UtcNow;

            ViewBag.CurrentMonth = currentDate.ToString("MMMM");

            CreditLineViewModel newLine = FinanceService.CreditLine(Manager, owed);

            return PartialView(newLine);
        }

        public ActionResult IndividualWinnings()
        {
            List<Manager> managerList = db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName).ToList();

            return View(managerList);
        }

        public ActionResult _IndividualLine(Guid Manager)
        {

            IndividualWinningsViewModel winnings = FinanceService.IndividualLine(Manager);

            return PartialView(winnings);
        }

        public ActionResult WinningsOverview()
        {
            if (db.Week.Count() != 0)
            {
                List<string> months = GeneralService.GetMonths();

                ViewBag.MonthList = months;
            }

            return View();
        }

        public ActionResult _FiversMeetingLine(string Month)
        {

            FiversLineViewModel fiveModel = FinanceService.FiversMeetingLine(Month);

            return PartialView(fiveModel);
        }

        public ActionResult _FiversManager()
        {
            var managers = db.Managers.OrderBy(x => x.ManagerName).ToList();

            decimal MgrNumber = managers.Count();

            ViewBag.MgrMidPoint = Math.Round(MgrNumber / 2);

            return PartialView(managers);
        }

        public ActionResult _FiversManagerLine(Manager manager)
        {
           FiversManagerLine fiveManagerModel = FinanceService.FiversManagerLineMethod(manager);

            return PartialView(fiveManagerModel);
        }

        public ActionResult _LeagueCupWon()
        {
            List<Transaction> LeagueAndCupWon = db.Transaction.Where(x => x.TransactionType == "LeagueCup").ToList();

            return PartialView();
        }

        public ActionResult _JackpotWinners()
        {
            List<Transaction> jackpotList = db.Transaction.Where(x => x.TransactionType == "Jackpot").ToList();

            return PartialView(jackpotList);
        }

        public ActionResult _CurrentJackpot()
        {
            if (db.Week.Count() > 0)
            {

                Week currentWeek = GeneralService.GetWeek(false);

                ViewBag.CurrentWeek = currentWeek.WeekNo;

                Jackpot jackpot = db.Jackpot.OrderBy(x => x.JackpotStartWk).Where(x => x.Active == true).FirstOrDefault();



                return PartialView(jackpot);
            }
            return PartialView();
        }

        public ActionResult _WeeklyWinners(int count)
        {
            if (db.Week.Count() > 0)
            {
                if (count == 0)
                {
                    count = db.Managers.Count();

                    ViewBag.NumofWinners = "Weekly Winners";
                }
                else
                {
                    ViewBag.NumofWinners = "Weekly Winners Last " + count.ToString();
                }

                List<Week> allWeeks = db.Week.OrderByDescending(x => x.WeekNo).Where(x => x.WeekCompleted == true).Take(count).ToList();
                List<Transaction> allWeeklyTransactions = db.Transaction.Where(x => x.TransactionType == "Weekly").OrderBy(x => x.Week.WeekNo).ToList();

                List<WeeklyWinnerSummaryViewModel> weeklyWinnerSummaries = new List<WeeklyWinnerSummaryViewModel>();

                allWeeks = allWeeks.OrderBy(x => x.WeekNo).ToList();

                foreach (var week in allWeeks)
                {
                    StringBuilder sb = new StringBuilder();

                    WeeklyWinnerSummaryViewModel weeklyWinner = new WeeklyWinnerSummaryViewModel
                    {
                        WeekNo = week.WeekNo
                    };

                    int x = 0;
                    foreach (var trans in allWeeklyTransactions)
                    {

                        if (trans.Week.WeekNo == week.WeekNo)
                        {
                            if (x == 0)
                            {
                                sb.Append(trans.Manager.ManagerName);
                                x = x + 1;
                            }
                            else
                            {
                                sb.Append(", ").Append(trans.Manager.ManagerName);
                            }
                        }
                    }

                    weeklyWinner.Winners = sb.ToString();

                    weeklyWinnerSummaries.Add(weeklyWinner);
                }

                return PartialView(weeklyWinnerSummaries);
            }
            return PartialView();
        }

        public ActionResult _LastFiversWon()
        {
            List<Transaction> FiversWon = db.Transaction.Where(x => x.TransactionType == "Fiver").OrderByDescending(x => x.TransactionDate).Take(5).ToList();
            if (FiversWon.Count > 0)
            {
                ViewBag.CurrentMonth = FiversWon[0].TransactionDate.ToString("MMMM");
                return PartialView(FiversWon);
            }

            return PartialView();
        }

        public ActionResult _JackpotSummary()
        {
            List<Transaction> jackpotTrans = db.Transaction.Where(x => x.TransactionType == "Jackpot").OrderBy(x => x.TransactionDate).ToList();
            List<Jackpot> jackpots = db.Jackpot.OrderBy(x => x.JackpotStartWk).ToList();

            JackpotViewModel jackpotViewModel = new JackpotViewModel
            {
                transactions = jackpotTrans,
                jackpots = jackpots
            };

            return PartialView(jackpotViewModel);
        }

        public ActionResult _LeagueCupSummary()
        {
            List<Prizes> prizeList = db.Prizes.OrderByDescending(x => x.PrizeAmount).ToList();

            return PartialView(prizeList);
        }

        public ActionResult _LeagueCupSummaryLine(string prizeType)
        {
            Transaction transaction = db.Transaction.Where(x => x.Notes == prizeType).FirstOrDefault();

            return PartialView(transaction);
        }

        public ActionResult WeekView()
        {
            List<Week> allWeeks = db.Week.ToList();

            return View(allWeeks);
        }
    }


}