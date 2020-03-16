using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.SL;
using LG.DLFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LG.DLFinance.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        DLFinanceContext db;
        IAdminService adminService;
        IMailService mailService;
        ITransactionService transService;

        public AdminController()
        {
            this.db = new DLFinanceContext();
            adminService = new AdminService(db, new GeneralService());
            mailService = new MailService();
            transService = new TransactionService();
        }

        public AdminController(DLFinanceContext context, IAdminService adminService, IMailService emailService, ITransactionService tranService)
        {
            this.db = context;
            this.adminService = adminService;
            this.mailService = emailService;
            this.transService = tranService;
        }

        // GET : Return List of Managers
        public ActionResult Managers()
        {
            List<Manager> Managers = db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName).ToList();

            return View(Managers);
        }

        //GET : Add a New Manager View

        public ActionResult AddManager()
        {
            ManagerViewModel manager = new ManagerViewModel();

            return View(manager);
        }

        //POST : Add a New Manager

        [HttpPost]
        public ActionResult AddManager(ManagerViewModel manager)
        {
            List<Emails> emailList = adminService.MapEmails(manager, false);
            
            Manager newManager = new Manager()

            {
                ManagerName = manager.ManagerName,
                Email = emailList
            };

                 
            db.Managers.Add(newManager);

            db.SaveChanges();

            return RedirectToAction("Managers");
        }

        // GET : Delete a Manager

        public ActionResult DeleteManager(Guid managerId)
        {
            Manager manager = db.Managers.Where(x => x.ManagerId == managerId).FirstOrDefault();

            return View(manager);
        }

        // POST : Delete a Manager
        [HttpPost]
        public ActionResult DeleteManager(Manager manager)
        {

            manager.Active = false;

            db.Entry(manager).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Managers");
        }

        // GET : Edit a Manager

        public ActionResult EditManager(Guid managerId)
        {
            Manager manager = db.Managers.Where(x => x.ManagerId == managerId).FirstOrDefault();

            ManagerViewModel managerView = new ManagerViewModel
            {
                ManagerId = managerId,
                ManagerName = manager.ManagerName,
                PrimaryEmail = manager.Email.Where(x => x.Primary == true).Select(y => y.EmailAddress).FirstOrDefault(),
                SecondaryEmail = manager.Email?.Where(x => x.Primary == false).Select(y => y.EmailAddress).FirstOrDefault(),
            };

            return View(managerView);
        }

        //POST : Edit a Manager
        [HttpPost]
        public ActionResult EditManager(ManagerViewModel manager)
        {
            var existingManager = db.Managers.Where(x => x.ManagerId == manager.ManagerId).FirstOrDefault();

            List<Emails> emailList = adminService.MapEmails(manager, true);

            existingManager.ManagerName = manager.ManagerName;
            existingManager.Email = emailList;

            db.Entry(existingManager).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Managers");
        }

        //Get : List of Fees
        [HttpGet]

        public ActionResult Fees()
        {
            List<Fees> feeList = db.Fees.ToList();

            return View(feeList);
        }

        //Get : Edit a Fee
        public ActionResult EditFee(Guid feesId)
        {

            Fees fee = db.Fees.Where(x => x.FeesId == feesId).FirstOrDefault();
            db.SaveChanges();
            
            return View(fee);
        }

        //Post : Edit a Fee

        [HttpPost]
        public ActionResult EditFee(Fees fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Fees");
        }

        //Get : Create a Fee
        public ActionResult CreateFee()
        {
            return View();
        }

        //Post : Create a Fee

        [HttpPost]
        public ActionResult CreateFee(Fees fee)
        {
            db.Fees.Add(fee);

            db.SaveChanges();

            return RedirectToAction("Fees");
        }


        public ActionResult SeasonView()
        {
            Week weeks = db.Week.FirstOrDefault();

            if (weeks == null)
            {
                ViewBag.NewSeason = "New";
            }

            return View();
        }
        [HttpPost]
        public ActionResult SeasonView(SeasonViewModel newSeason)
        {

            DateTime startDate = newSeason.WeekStartDate;

            for (int i = 1; i <= newSeason.NumberofWeeks; i++)
            {
                Week newWeek = new Week
                {
                    WeekNo = i,
                    WeekStartDate = startDate,
                    WeekEndDate = startDate.AddDays(7).AddMinutes(-1)
                };

                db.Week.Add(newWeek);

                startDate = startDate.AddDays(7);
            }

            Jackpot jackpot = new Jackpot();

            jackpot.JackpotStartWk = 1;

            

            db.Jackpot.Add(jackpot);

            db.SaveChanges();

            return View();
        }

        public PartialViewResult _CurrentSeason()
        {
            SeasonViewModel currentSeason = new SeasonViewModel
            {
                NumberofManagers = db.Managers.Where(x => x.Active == true).Count(),
                NumberofWeeks = db.Week.Count(),
                WeekStartDate = db.Week.Where(x => x.WeekNo == 1).Select(y => y.WeekStartDate).FirstOrDefault()
            };



            return PartialView(currentSeason);
        }

        //End a Season Method
        public ActionResult Delete()
        {
            adminService.LogHistory();

            var rows = from o in db.Week
                       select o;
            foreach (var row in rows)
            {
                db.Week.Remove(row);
            }

            var JRows = from o in db.Jackpot
                       select o;
            foreach (var jRow in JRows)
            {
                db.Jackpot.Remove(jRow);
            }

            var tRows = from o in db.Transaction
                       select o;
            foreach (var tRow in tRows)
            {
                db.Transaction.Remove(tRow);
            }

            var zRows = from o in db.Managers
                        select o;
            foreach (var zRow in zRows)
            {
                zRow.Balance = 0;
            }

            //DEBUG
            //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[DLFinanceContext].[DL].[Week]', RESEED, 0)");
            //LIVE
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[LGDLFinanceDB].[DL].[Week]', RESEED, 0)");

            db.SaveChanges();
          
         return RedirectToAction("seasonView");
        }

        //Get : List of Prizes
        [HttpGet]

        public ActionResult Prizes()
        {
            List<Prizes> prizeList = db.Prizes.OrderByDescending(x => x.PrizeAmount).ToList();

            return View(prizeList);
        }

        //Get : Edit a Prize
        public ActionResult EditPrizes(Guid prizeId)
        {

            Prizes prize = db.Prizes.Where(x => x.PrizesId == prizeId).FirstOrDefault();
            db.SaveChanges();

            return View(prize);
        }

        //Post : Edit a Prize

        [HttpPost]
        public ActionResult EditPrizes(Prizes prize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prize).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Prizes");
        }

        //Get : Create a Prize
        public ActionResult CreatePrize()
        {
            return View();
        }

        //Post : Create a Prize

        [HttpPost]
        public ActionResult CreatePrize(Prizes prize)
        {
            db.Prizes.Add(prize);

            db.SaveChanges();

            return RedirectToAction("Prizes");
        }

        public ActionResult EmailViewSelect()
        {

            ViewBag.ManagerList = new SelectList(db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName), "ManagerId", "ManagerName");

            return View();
        }

        public ActionResult EmailView(string SelectedManager)
        {
            if (!string.IsNullOrEmpty(SelectedManager))
            {
                Guid mgrId = Guid.Parse(SelectedManager);
                Manager manager = db.Managers.Where(x => x.ManagerId == mgrId).FirstOrDefault();

                EmailViewModel emailView = adminService.GetEmailData(manager);

                return View(emailView);
            }
            else
            {
                return RedirectToAction("EmailViewSelect");
            }

        }

        public ActionResult Send()
        {
            List<Manager> managerList = db.Managers.Where(x => x.Active == true).ToList();

            List<EmailViewModel> emailViewModels = new List<EmailViewModel>();

            foreach (var manager in managerList)
            {
                EmailViewModel mailModel = adminService.GetEmailData(manager);
                emailViewModels.Add(mailModel);
            }

            mailService.Send(emailViewModels, this.ControllerContext);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RunWeeklyAPI()
        {
            transService.WeeklyAPI();

            return RedirectToAction("Index", "Home");
        }

    }
}