using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LG.DLFinance.SL
{
    public class AdminService : IAdminService
    {

        DLFinanceContext db;

        IGeneralService generalService;

        public AdminService()
        {
            this.db = new DLFinanceContext();
            this.generalService = new GeneralService(db);
        }
        public AdminService(DLFinanceContext context, IGeneralService generalService)
        {
            this.db = context;
            this.generalService = generalService;
        }



        public List<Emails> MapEmails(ManagerViewModel manager, bool isEdit)
        {
            List<Emails> emailList = new List<Emails>();

            if (isEdit == true)
            {
                Emails primaryEmail = new Emails();
                Emails secondaryEmail = new Emails();

                primaryEmail = db.Managers.Where(x => x.ManagerId == manager.ManagerId && x.Active == true).Select(y => y.Email.Where(p => p.Primary == true).FirstOrDefault()).FirstOrDefault();

                if (!string.IsNullOrEmpty(manager.SecondaryEmail))
                {
                   secondaryEmail = db.Managers.Where(x => x.ManagerId == manager.ManagerId && x.Active == true).Select(y => y.Email.Where(p => p.Primary == false).FirstOrDefault()).FirstOrDefault();
                }

                if(primaryEmail.EmailAddress != manager.PrimaryEmail)
                {
                    db.Emails.Remove(primaryEmail);
                    db.SaveChanges();

                    Emails primary = new Emails
                    {
                        EmailAddress = manager.PrimaryEmail,
                        Primary = true
                    };

                    emailList.Add(primary);
                }

                if(!string.IsNullOrEmpty(manager.SecondaryEmail) && secondaryEmail != null )
                {
                    if (manager.SecondaryEmail != secondaryEmail.EmailAddress)
                    {
                        db.Emails.Remove(secondaryEmail);
                        db.SaveChanges();

                        Emails secondary = new Emails
                        {
                            EmailAddress = manager.SecondaryEmail,
                            Primary = false
                        };
                        emailList.Add(secondary);
                    }
                }
                else if(secondaryEmail == null && !string.IsNullOrEmpty(manager.SecondaryEmail))
                {
                    Emails secondary = new Emails
                    {
                        EmailAddress = manager.SecondaryEmail,
                        Primary = false
                    };
                    emailList.Add(secondary);
                }
            }
            else
            {

                Emails primary = new Emails
                {
                    EmailAddress = manager.PrimaryEmail,
                    Primary = true
                };

                emailList.Add(primary);


                if (!string.IsNullOrEmpty(manager.SecondaryEmail))
                {
                    Emails secondary = new Emails
                    {
                        EmailAddress = manager.SecondaryEmail,
                        Primary = false
                    };
                    emailList.Add(secondary);
                }
            }
            return emailList;
        }

        public void LogHistory()
        {

            

            List<Transaction> tranList = db.Transaction.ToList();

            List<Transaction> biggestJackpot = new List<Transaction>();

            List<Transaction> fivers = new List<Transaction>();

            List<Transaction> weekly = new List<Transaction>();

            List<Transaction> all = new List<Transaction>();

            string biggestJackpotString = "";

            string leastFivers = "";

            string mostFivers = "";

            string mostWeekly = "";

            string leastWeekly = "";

            string mostWon = "";


            foreach (var transaction in tranList)
            {
                if (transaction.TransactionType == "Jackpot")
                {
                    biggestJackpot.Add(transaction);
                    all.Add(transaction);
                }
                else if(transaction.TransactionType == "Fiver")
                {
                    fivers.Add(transaction);
                    all.Add(transaction);
                }
                else if(transaction.TransactionType == "Weekly")
                {
                    weekly.Add(transaction);
                    all.Add(transaction);
                }
                else if(transaction.TransactionType == "League or Cup")
                {
                    all.Add(transaction);
                }
            };

            //biggest Jackpot Calculation

            if (biggestJackpot.Count > 0)
            {
                biggestJackpot = biggestJackpot.OrderByDescending(x => x.Value).ToList();

                decimal LastValue = 0;

                for (int i = 0; i < biggestJackpot.Count; i++)
                {
                    Transaction BJ = biggestJackpot[i];

                    if (i == 0)
                    {
                        biggestJackpotString = BJ.Manager.ManagerName + " - £" + BJ.Value.ToString();
                    }
                    else
                    {
                        if (BJ.Value == LastValue)
                        {
                            biggestJackpotString = BJ.Manager.ManagerName + ", " + biggestJackpotString;
                        }

                    }
                    LastValue = BJ.Value;

                };
            }

            //most fivers Calculation

            var fiverCount = fivers.GroupBy(x => x.Manager.ManagerName).Select(c => new { Key = c.Key, total = c.Count() }).OrderByDescending(p => p.total).ToList();

            for(int i = 0; i <= fiverCount.Count(); i++)
            {
                    var activeFiver = fiverCount[i];

                    decimal amount = activeFiver.total * 5;

                if (i == 0)
                {
                    mostFivers = activeFiver.Key + " - £" + amount;
                }
                else if(i != 0 && fiverCount[i-1].total == activeFiver.total)
                {
                    mostFivers = activeFiver.Key + ", " + mostFivers;
                }
                else
                {
                    break;
                }
            }

            //least fivers Calculation

            fiverCount = fiverCount.OrderBy(p => p.total).ToList();

            for (int i = 0; i <= fiverCount.Count(); i++)
            {
                var activeFiver = fiverCount[i];

                decimal amount = activeFiver.total * 5;

                if (i == 0)
                {
                    leastFivers = activeFiver.Key + " - £" + amount;
                }
                else if (i != 0 && fiverCount[i - 1].total == activeFiver.total)
                {
                    leastFivers = activeFiver.Key + ", " + leastFivers;
                }
                else
                {
                    break;
                }
            }

            //most Weekly calculations

            var weekCount = weekly.GroupBy(x => x.Manager.ManagerName).Select(c => new { Key = c.Key, total = c.Sum(s => s.Value) }).OrderByDescending(p => p.total).ToList();

            for (int i = 0; i <= weekCount.Count(); i++)
            {
                var activeWeek = weekCount[i];

                if (i == 0)
                {
                    mostWeekly = activeWeek.Key + " - £" + activeWeek.total;
                }
                else if (i != 0 && weekCount[i - 1].total == activeWeek.total)
                {
                    mostWeekly = activeWeek.Key + ", " + mostWeekly;
                }
                else
                {
                    break;
                }
            }

            //least Weekly calculations

            weekCount = weekCount.OrderBy(x => x.total).ToList();

            for (int i = 0; i <= weekCount.Count(); i++)
            {
                var activeWeek = weekCount[i];

                if (i == 0)
                {
                    leastWeekly = activeWeek.Key + " - £" + activeWeek.total;
                }
                else if (i != 0 && weekCount[i - 1].total == activeWeek.total)
                {
                    leastWeekly = activeWeek.Key + ", " + leastWeekly;
                }
                else
                {
                    break;
                }
            }

            //most Money calculation

            var allCount = all.GroupBy(x => x.Manager.ManagerName).Select(c => new { Key = c.Key, total = c.Sum(s => s.Value) }).OrderByDescending(p => p.total).ToList();

            for (int i = 0; i <= allCount.Count(); i++)
            {
                var active = allCount[i];

                if (i == 0)
                {
                    mostWon = active.Key + " - £" + active.total;
                }
                else if (i != 0 && weekCount[i - 1].total == active.total)
                {
                    mostWon = active.Key + ", " + mostWon;
                }
                else
                {
                    break;
                }
            }


            History seasonHistory = new History
            {
                historyId = Guid.NewGuid(),
                year = DateTime.Now.Year,
                numberOfManagers = db.Managers.Where(x => x.Active == true).Count(),
                biggestJackpot = biggestJackpotString,
                mostFivers = mostFivers,
                leastFivers = leastFivers,
                mostWeekly = mostWeekly,
                leastWeekly = leastWeekly,
                mostMoney = mostWon
            };

            db.History.Add(seasonHistory);

            db.SaveChanges();
        }



        public EmailViewModel GetEmailData(Manager manager)
        {
            DateTime currentDate = DateTime.UtcNow.AddDays(-14);
            DateTime actualDate = DateTime.UtcNow;

            //use general service, get weeks then select all transactions that are weekly matching those weeks. If more than one then build a string....

            List<int> weekList = generalService.GetWeekNumbers(actualDate);
            List<WeeklyWinnerSummaryViewModel> listWeeklyVM = new List<WeeklyWinnerSummaryViewModel>();
            List<Transaction> weeklyTrans = db.Transaction.Where(x => x.TransactionType == "Weekly").ToList();

            foreach(var week in weekList)
            {
                List<Transaction> filteredWeeklyTrans = weeklyTrans.Where(x => x.Week.WeekNo == week).ToList();
                String winnerString = "";
                int i = 0;

                foreach(var filteredWeek in filteredWeeklyTrans)
                {
                    if (i == 0)
                    {
                        winnerString = filteredWeek.Manager.ManagerName;
                    }
                    else
                    {
                        winnerString = winnerString + ", " + filteredWeek.Manager.ManagerName;
                    }
                    i++;
                }

                WeeklyWinnerSummaryViewModel weeklyWinnerVM = new WeeklyWinnerSummaryViewModel
                {
                    WeekNo = week,
                    Winners = winnerString
                };

                listWeeklyVM.Add(weeklyWinnerVM);
            }
            

            EmailViewModel emailData = new EmailViewModel
            {
                ManagerList = db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName).ToList(),
                CurrentMonth = currentDate.ToString("MMMM"),
                CurrentWeek = generalService.GetWeek(false),
                currentJackpot = db.Jackpot.Where(x => x.Active == true).FirstOrDefault(),
                wonJackpot = db.Transaction.Where(x => x.TransactionDate.Month == currentDate.Month && x.TransactionType == "Jackpot").ToList(),
                weeklyWinners = listWeeklyVM,
                monthTransactions = db.Transaction.Where(x => x.TransactionDate.Month == currentDate.Month && x.Manager.ManagerName == manager.ManagerName).OrderBy(z => z.TransactionDate).ToList(),
                Manager = manager

            };
                       
            return emailData;
        }
    }
}