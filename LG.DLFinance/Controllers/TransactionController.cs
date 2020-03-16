using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.SL;
using LG.DLFinance.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.Controllers
{
  
  
    public class TransactionController : Controller
    {
        DLFinanceContext db;

        IFinanceService FinanceService;
        ITransactionService TranService;
        IGeneralService GeneralService;

        public TransactionController()
        {
            this.db = new DLFinanceContext();
            this.FinanceService = new FinanceService(db, new GeneralService());
            this.TranService = new TransactionService(db, new GeneralService());
            this.GeneralService = new GeneralService();
        }

        public TransactionController(DLFinanceContext context, IFinanceService financeService, ITransactionService tranService, IGeneralService generalService)
        {
            this.db = context;
            this.FinanceService = financeService;
            this.TranService = tranService;
            this.GeneralService = generalService;
        }


        // GET: Finance
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.SeasonExist = db.Week.Count();

            return View();
        }

        // GET: Finance
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Index(string transactionType)
        {

            switch (transactionType)
            {
                case "adhoc":
                    return RedirectToAction("AdhocPayment");

                case "jackpotCarryOver":
                    return RedirectToAction("JackpotCarryOver");

                case "weekly":
                    return RedirectToAction("WeeklyPrize");

                case "fivers":
                    return RedirectToAction("Fivers");

                case "jackpot":
                    return RedirectToAction("JackpotPayment");

                case "end":
                    return RedirectToAction("LeagueCupPayment");

                default:
                    break;
            }

            ViewBag.SeasonExist = db.Week.Count();

            return View();
        }

        //return adhoc view
        [Authorize(Roles = "Admin")]
        public ActionResult AdhocPayment()
        {
            var ManagerList = new List<SelectListItem>();

            ManagerList.Add(new SelectListItem() { Text = "Select Manager...", Value = string.Empty});

            List<Manager> allManagers = db.Managers.Where(p => p.Active == true).OrderBy(x => x.ManagerName).ToList();

            foreach (var mgr in allManagers)
            {
                ManagerList.Add(new SelectListItem() { Text = mgr.ManagerName, Value = mgr.ManagerId.ToString() });
            }

            PaymentViewModel payment = new PaymentViewModel()
            {
                TransactionType = "Ad-Hoc",
                ManagerList = ManagerList
            };

            return View(payment);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        //post for adhoc payment

        public ActionResult AdhocPayment(PaymentViewModel payment)
        {
            TranService.AdhocProcess(payment);

            return RedirectToAction("Index");
        }

        //return weekly prize 
        [Authorize(Roles = "Admin")]
        public ActionResult WeeklyPrize()
        {
            var managerList = new SmallPrizeListViewModel();
            managerList.SmallPrizeList = new List<SmallPrizeViewModel>();

            List<Manager> allManagers = db.Managers.Where(p => p.Active == true).OrderBy(x => x.ManagerName).ToList();

            foreach (var m in allManagers)
            {
                managerList.SmallPrizeList.Add(new SmallPrizeViewModel() { ManagerId = m.ManagerId, ManagerName = m.ManagerName, Checkbox = false });
            }

            return View(managerList);

        }

        //return adhoc view
        [Authorize(Roles = "Admin")]
        public ActionResult JackpotCarryOver()
        {
            
            PaymentViewModel payment = new PaymentViewModel()
            {
                TransactionType = "Jackpot Carry Over"
            };

            return View(payment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        //post for adhoc payment

        public ActionResult JackpotCarryOver(PaymentViewModel payment)
        {
            TranService.JackpotCarryOver(payment);

            return RedirectToAction("Index");
        }



        //post weekly prize
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult WeeklyPrize(SmallPrizeListViewModel mgrToPay)
        {
            TranService.WeeklyProcess(mgrToPay);

            return RedirectToAction("Index");
        }

        //return fivers view
        [Authorize(Roles = "Admin")]
        public ActionResult Fivers()
        {
            var managerList = new SmallPrizeListViewModel();
            managerList.SmallPrizeList = new List<SmallPrizeViewModel>();

            List<Manager> allManagers = db.Managers.Where(p => p.Active == true).OrderBy(x => x.ManagerName).ToList();

            foreach (var m in allManagers)
            {
                managerList.SmallPrizeList.Add(new SmallPrizeViewModel() { ManagerId = m.ManagerId, ManagerName = m.ManagerName, Checkbox = false });
            }

            return View(managerList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Fivers(SmallPrizeListViewModel mgrToPay)
        {
            TranService.FiverProcess(mgrToPay);

            return RedirectToAction("Index");
        }


        
        //list of transactions
        public ActionResult AllTransactions(string ManagerList, string MonthList, int page = 1, int pageSize  = 50)
        {
            List<Transaction> allTrans = new List<Transaction>();

           

            if (string.IsNullOrEmpty(ManagerList) && string.IsNullOrEmpty(MonthList))
            {
                 allTrans = db.Transaction.OrderByDescending(x => x.TransactionDate).ToList();
            }
            else if(!string.IsNullOrEmpty(ManagerList) && string.IsNullOrEmpty(MonthList))
            {
                Guid mgrId = Guid.Parse(ManagerList);
                allTrans = db.Transaction.Where(x => x.ManagerId == mgrId).OrderByDescending(x => x.TransactionDate).ToList();
            }
            else if (string.IsNullOrEmpty(ManagerList) && !string.IsNullOrEmpty(MonthList))
            {
                int iMonthNo = Convert.ToDateTime("01-" + MonthList + "-2018").Month;
                allTrans = db.Transaction.Where(x => x.TransactionDate.Month == iMonthNo).OrderByDescending(x => x.TransactionDate).ToList();
            }
            else
            {
                Guid mgrId = Guid.Parse(ManagerList);
                int iMonthNo = Convert.ToDateTime("01-" + MonthList + "-2018").Month;
                allTrans = db.Transaction.Where(x => x.TransactionDate.Month == iMonthNo && x.ManagerId == mgrId).OrderByDescending(x => x.TransactionDate).ToList();
            }

            ViewBag.ManagerList = new SelectList(db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName), "ManagerId", "ManagerName");
            ViewBag.MonthList = new SelectList(GeneralService.GetMonths());

            return View(new PagedList<Transaction>(allTrans, page, pageSize));
        }

        //edit a transaction
        [Authorize(Roles = "Admin")]
        public ActionResult EditTransaction(Guid TransactionId)
        {

            Transaction trans = db.Transaction.Where(X => X.TransactionId == TransactionId).FirstOrDefault();

            ViewBag.ManagerList = new SelectList(db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName), "ManagerId", "ManagerName", trans.ManagerId);

            return View(trans);
        }

        //post for edit a transaction
        [Authorize(Roles = "Admin")]
        [HttpPost]

        public ActionResult EditTransaction(Transaction transaction)
        {
            TranService.ChangedTransaction(transaction, false);

            var exisitingTran = db.Transaction.Where(x => x.TransactionId == transaction.TransactionId).FirstOrDefault();

            exisitingTran.TransactionDate = transaction.TransactionDate;
            exisitingTran.Value = transaction.Value;
            exisitingTran.Notes = transaction.Notes;
            exisitingTran.ManagerId = transaction.ManagerId;
            exisitingTran.Manager = db.Managers.Where(x => x.ManagerId == transaction.ManagerId).FirstOrDefault();

            db.Entry(exisitingTran).State = System.Data.Entity.EntityState.Modified;

            

            db.SaveChanges();

            return RedirectToAction("allTransactions");
        }


        // GET : Delete a Transaction
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTransaction(Guid TransactionId)
        {
            Transaction transaction = db.Transaction.Where(x => x.TransactionId == TransactionId).FirstOrDefault();

            return View(transaction);
        }

        // POST : Delete a Transaction
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteTransaction(Transaction transaction)
        {
            TranService.ChangedTransaction(transaction, true);

            db.Transaction.Remove(transaction);
            
            db.SaveChanges();

            return RedirectToAction("allTransactions");
        }

        //return jackpot view
        [Authorize(Roles = "Admin")]
        public ActionResult JackpotPayment()
        {
            var ManagerList = new List<SelectListItem>();

            ManagerList.Add(new SelectListItem() { Text = "Select Manager...", Value = string.Empty });

            List<Manager> allManagers = db.Managers.Where(p => p.Active == true).OrderBy(x => x.ManagerName).ToList();

            foreach (var mgr in allManagers)
            {
                ManagerList.Add(new SelectListItem() { Text = mgr.ManagerName, Value = mgr.ManagerId.ToString() });
            }

            PaymentViewModel payment = new PaymentViewModel()
            {
                TransactionType = "Jackpot",
                ManagerList = ManagerList
            };

            return View(payment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        //post for adhoc payment

        public ActionResult JackpotPayment(PaymentViewModel payment)
        {
            TranService.JackpotProcess(payment);

            return RedirectToAction("Index");
        }

        //return league/cup view
        [Authorize(Roles = "Admin")]
        public ActionResult LeagueCupPayment()
        {
            var ManagerList = new List<SelectListItem>();

            LgCupWinnersViewModel leagueCup = new LgCupWinnersViewModel();

            ManagerList.Add(new SelectListItem() { Text = "Select Manager...", Value = string.Empty });

            List<Manager> allManagers = db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName).ToList();


            foreach (var mgr in allManagers)
            {
                ManagerList.Add(new SelectListItem() { Text = mgr.ManagerName, Value = mgr.ManagerId.ToString() });
            }

            List<Transaction> currentPaidPrizes = db.Transaction.Where(x => x.TransactionType == "League or Cup").ToList();

            foreach (var PrizeItem in db.Prizes.OrderByDescending(x => x.PrizeAmount))
            {
 
                LgCupWinnersLineViewModel league = new LgCupWinnersLineViewModel()
                {
                    PrizeSelection = PrizeItem,
                    ManagerList = ManagerList
                };

                leagueCup.lgCupWinnersLineViewModels.Add(league);
            }

            return View(leagueCup);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult LeagueCupPayment(LgCupWinnersViewModel lgCupWinners)
        {

            TranService.LgCupProcess(lgCupWinners);

            return RedirectToAction("Index");
        }
    }
}