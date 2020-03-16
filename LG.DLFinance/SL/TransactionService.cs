using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LG.DLFinance.SL
{
    public class TransactionService : ITransactionService
    {
        DLFinanceContext db;

        IGeneralService generalService;

        public TransactionService()
        {
            this.db = new DLFinanceContext();
            this.generalService = new GeneralService(db);
        }

        public TransactionService(DLFinanceContext context, IGeneralService generalService)
        {
            this.db = context;
            this.generalService = generalService;
        }
        //process all all types of transaction
        internal void processTransaction(Guid managerId, decimal amount, string type, bool endWeek, string notes)
        {
            Week currentWeek = generalService.GetWeek(false);
            Manager selectedManager = db.Managers.Where(x => x.ManagerId == managerId).FirstOrDefault();

            Transaction newTrans = new Transaction
            {
                ManagerId = managerId,
                TransactionDate = DateTime.UtcNow,
                Manager = selectedManager,
                TransactionType = type,
                Value = amount,
                WeekId = currentWeek.WeekNo,
                Notes = notes
            };

            selectedManager.Balance = selectedManager.Balance + amount;
            

            db.Transaction.Add(newTrans);
            db.Entry(selectedManager).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            if(endWeek == true)
            {
                generalService.UpdateWeek(currentWeek);
            }
        }

        //process an adhoc payment
        public void AdhocProcess(PaymentViewModel payment)
        {

            processTransaction(Guid.Parse(payment.PickedManager), payment.Amount, payment.TransactionType, false, payment.Notes);

        }

        

        //process a five fivers transaction
        public void FiverProcess(SmallPrizeListViewModel fiverViewModels)
        {

            foreach (var mgr in fiverViewModels.SmallPrizeList.Where(x => x.Checkbox == true))
            {
                processTransaction(mgr.ManagerId, 5, "Fiver", false, "");
            }

        }

        //process a weekly prize (manual)C:\LGSource2\LG.DLFinance\LG.DLFinance\ApplicationPreload.cs
        public void WeeklyProcess(SmallPrizeListViewModel weeklyViewModels)
        {
            decimal prize = 6;

            int winners = weeklyViewModels.SmallPrizeList.Where(x => x.Checkbox == true).Count();

            prize = prize / winners;

            List<SmallPrizeViewModel> checkedList = weeklyViewModels.SmallPrizeList.Where(x => x.Checkbox == true).ToList();

            for (int i = 0; i < checkedList.Count(); i++)
            {
                if (i == checkedList.Count - 1)
                {
                    JackpotAdd();
                    processTransaction(checkedList[i].ManagerId, prize, "Weekly", true, "");
                }
                else
                {
                    processTransaction(checkedList[i].ManagerId, prize, "Weekly", false, "");
                }
            }
        }


        //process a jackpot payment
        public void JackpotProcess(PaymentViewModel payment)
        {
            Jackpot currentJackpot = db.Jackpot.Where(x => x.Active == true).OrderBy(x => x.JackpotId).FirstOrDefault();

            payment.Amount = currentJackpot.JackpotValue;

            //End the current Jackpot

            EndCurrentJackpot(currentJackpot);

            processTransaction(Guid.Parse(payment.PickedManager), payment.Amount, payment.TransactionType, false, "");

        }

        public void JackpotAdd()
        {
            Jackpot currentJackpot = db.Jackpot.Where(x => x.Active == true).OrderByDescending(x => x.JackpotId).FirstOrDefault();

            if (currentJackpot.JackpotValue < 50)
            {
                currentJackpot.JackpotValue = currentJackpot.JackpotValue + 2;
                db.Entry(currentJackpot).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                Jackpot jackpot = new Jackpot();
                Week currentWeek = generalService.GetWeek(false);
                jackpot.JackpotStartWk = currentWeek.WeekNo;
                jackpot.JackpotValue = 2;
                db.Jackpot.Add(jackpot);
            }

            db.SaveChanges();
        }
        
        public void EndCurrentJackpot(Jackpot currentJackpot)
        {
            Week currentWeek = generalService.GetWeek(true);

            currentJackpot.Active = false;
            currentJackpot.JackpotEndWk = currentWeek.WeekNo;

            db.Entry(currentJackpot).State = System.Data.Entity.EntityState.Modified;

            if(db.Jackpot.Where(x=> x.Active == true).Count() == 0)
                {
                Jackpot newJackpot = new Jackpot();

                db.Jackpot.Add(newJackpot);
                }

            db.SaveChanges();
        }

        public void WeeklyAPI()
        {
            string json;

            IWebProxy proxy = WebRequest.DefaultWebProxy;
            proxy.Credentials = CredentialCache.DefaultCredentials;

            using (WebClient client = new WebClient())
            {
                client.Proxy = proxy;
                json = client.DownloadString("https://dreamleaguefantasyfootball.co.uk/api/data/winners");
            }

      
            List<WeeklyAPI> weekly = JsonConvert.DeserializeObject<List<WeeklyAPI>>(json);

            List<Manager> managerList = db.Managers.ToList();

            List<Transaction> weeklyTransactionList = db.Transaction.Where(x => x.TransactionType == "Weekly").ToList();

  
            foreach (var groups in weekly.GroupBy(x => x.WeekNo))
            {

                List<Transaction> alreadyExisting = weeklyTransactionList.Where(x => x.WeekId == groups.Key).ToList();
                               
                   


                if (alreadyExisting.Count == 0)
                {

                    decimal amount = 6 / groups.Count();

                        int z = 1;

                        foreach (var group in groups)
                        {

                            if (groups.Count() == z)
                            {
                                JackpotAdd();
                                processTransaction(managerList.Where(x => x.ManagerName == group.Winner).Select(y => y.ManagerId).FirstOrDefault(), amount, "Weekly", true, "");
                            }
                            else
                            {
                                processTransaction(managerList.Where(x => x.ManagerName == group.Winner).Select(y => y.ManagerId).FirstOrDefault(), amount, "Weekly", false, "");

                                z++;
                            }
                        }
                 
                }

                else if (alreadyExisting.Count > 0)
                {
                    List<string> existingWinnerList = new List<String>();
                    List<string> newWinnerList = new List<String>(); ;

                    foreach (var mgr in alreadyExisting)
                    {
                        existingWinnerList.Add(mgr.Manager.ManagerName);
                    }
                    foreach (var winner in groups)
                    {
                        newWinnerList.Add(winner.Winner.ToString());
                    }

                    if (!Enumerable.SequenceEqual(existingWinnerList.OrderBy(t => t), newWinnerList.OrderBy(q => q)))
                    {
                        foreach (var item in alreadyExisting)
                        {
                            ChangedTransaction(item, true);
                            generalService.ResetWeek(item.WeekId);
                            db.Transaction.Remove(item);
                                                   
                        }

                            decimal amount = 6 / newWinnerList.Count();

                            int g = 1;

                            foreach (var group in groups)
                            {
                                if (groups.Count() == g)
                                {
                                    processTransaction(managerList.Where(x => x.ManagerName == group.Winner).Select(y => y.ManagerId).FirstOrDefault(), amount, "Weekly", true, "");
                                }
                                else
                                {
                                    processTransaction(managerList.Where(x => x.ManagerName == group.Winner).Select(y => y.ManagerId).FirstOrDefault(), amount, "Weekly", false, "");
                                    g++;
                                }
                            }
             

                    }
                }

                
            }


        }

        public void ChangedTransaction(Transaction transaction, bool delete)
        {


                Manager mgr = db.Managers.Where(x => x.ManagerId == transaction.ManagerId).FirstOrDefault();

                if(delete == true)
                {
                    mgr.Balance = mgr.Balance - transaction.Value;
                }
                else
                {
                    Transaction oldTransaction = db.Transaction.Where(x => x.TransactionId == transaction.TransactionId).FirstOrDefault();

                    decimal valueChange = oldTransaction.Value - transaction.Value;

                    if(valueChange > 0)
                    {
                        mgr.Balance = mgr.Balance - valueChange;
                    }
                    else if(valueChange < 0)
                    {
                        mgr.Balance = mgr.Balance + (valueChange * -1);
                    }
                }
            

        }

        public void LgCupProcess(LgCupWinnersViewModel LeagueCup)
        {


            List<Transaction> existingList = db.Transaction.Where(x => x.TransactionType == "League or Cup").ToList();
            List<Prizes> prizes = db.Prizes.ToList();
            Week currentWk = generalService.GetWeek(false);

            foreach(LgCupWinnersLineViewModel lgcupLine in LeagueCup.lgCupWinnersLineViewModels)
            {
                if (lgcupLine.PickedManager != null)
                {
                    Prizes prizeWon = prizes.Where(x => x.PrizesId == lgcupLine.PrizeSelection.PrizesId).FirstOrDefault();
                    Transaction existingTran = existingList.Where(x => x.Notes == prizeWon.PrizeType).FirstOrDefault();

                    if (existingTran != null)
                    {
                        db.Transaction.Remove(existingTran);
                    }

                    processTransaction(Guid.Parse(lgcupLine.PickedManager), prizeWon.PrizeAmount, "League or Cup", false, prizeWon.PrizeType);
                }
            }
        }

        //process a jackpot carry over

        public void JackpotCarryOver(PaymentViewModel payment)
        {
            Guid managerID = db.Managers.Where(x => x.ManagerName == "Jackpot Carry Over").Select(p => p.ManagerId).FirstOrDefault();

            Jackpot jackpot = db.Jackpot.Where(x => x.Active == true).FirstOrDefault();

            jackpot.JackpotValue = jackpot.JackpotValue + Decimal.ToInt32(payment.Amount);

            db.Entry(jackpot).State = System.Data.Entity.EntityState.Modified;

            processTransaction(managerID, payment.Amount, payment.TransactionType, false, "");

        }
    }
}