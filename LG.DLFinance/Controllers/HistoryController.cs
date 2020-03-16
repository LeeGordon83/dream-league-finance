using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.Controllers
{
    public class HistoryController : Controller
    {

        DLFinanceContext db;

        IAdminService AdminService;


        public HistoryController()
        {
            this.db = new DLFinanceContext();
            this.AdminService = new AdminService(db, new GeneralService());
        }

        public HistoryController(DLFinanceContext context, IAdminService adminService)
        {
            this.db = context;
            this.AdminService = adminService;
        }

        public ActionResult History()
        {
            List<History> historyList = db.History.OrderByDescending(x => x.year).ToList();

            return View(historyList);
        }

        public ActionResult RunSeasonHistory()
        {
            AdminService.LogHistory();

            return RedirectToAction("History");
        }
    }
}