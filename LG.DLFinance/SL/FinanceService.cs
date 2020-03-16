using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.SL
{
    public class FinanceService : IFinanceService
    {
        DLFinanceContext db;

        IGeneralService generalService;

        public FinanceService()
        {
            this.db = new DLFinanceContext();
            this.generalService = new GeneralService(db);
        }

        public FinanceService(DLFinanceContext context, IGeneralService generalService)
        {
            this.db = context;
            this.generalService = generalService;
        }

        
        //method to create main paid in view
        public PaidInViewModel PaidIn()
        {


            PaidInViewModel paidInView = new PaidInViewModel()
            {
                ManagerList = db.Managers.Where(x => x.Active == true).OrderBy(x => x.ManagerName).ToList(),
                MonthList = generalService.GetMonths()

            };

            return paidInView;
        }

        //method to create all paid in lines for a manager for paid in view
        public PaidInLineViewModel PaidInLine(Guid Manager)
        {

            List<Transaction> allTrans = db.Transaction.Where(x => x.Manager.ManagerId == Manager).ToList();

            decimal AugustPaidIn = 0;
            decimal AugustWon = 0;
            decimal SepPaidIn = 0;
            decimal SepWon = 0;
            decimal OctPaidIn = 0;
            decimal OctWon = 0;
            decimal NovPaidIn = 0;
            decimal NovWon = 0;
            decimal DecPaidIn = 0;
            decimal DecWon = 0;
            decimal JanPaidIn = 0;
            decimal JanWon = 0;
            decimal FebPaidIn = 0;
            decimal FebWon = 0;
            decimal MarPaidIn = 0;
            decimal MarWon = 0;
            decimal AprilPaidIn = 0;
            decimal AprilWon = 0;
            decimal MayPaidIn = 0;
            decimal MayWon = 0;


            foreach (var trans in allTrans)
            {

                if (trans.TransactionDate.Month == 8)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        AugustPaidIn = AugustPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        AugustWon = AugustWon + trans.Value;
                    }
                }


                else if (trans.TransactionDate.Month == 9)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        SepPaidIn = SepPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        SepWon = SepWon + trans.Value;
                    }
                }


                else if (trans.TransactionDate.Month == 10)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        OctPaidIn = OctPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        OctWon = OctWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 11)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        NovPaidIn = NovPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        NovWon = NovWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 12)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        DecPaidIn = DecPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        DecWon = DecWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 1)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        JanPaidIn = JanPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        JanWon = JanWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 2)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        FebPaidIn = FebPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        FebWon = FebWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 3)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        MarPaidIn = MarPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        MarWon = MarWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 4)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        AprilPaidIn = AprilPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        AprilWon = AprilWon + trans.Value;
                    }
                }

                else if (trans.TransactionDate.Month == 5)
                {
                    if (trans.TransactionType == "Ad-Hoc")
                    {
                        MayPaidIn = MayPaidIn + trans.Value;
                    }
                    else if (trans.TransactionType == "Fiver" || trans.TransactionType == "Weekly")
                    {
                        MayWon = MayWon + trans.Value;
                    }
                }

            }

            PaidInLineViewModel newLine = new PaidInLineViewModel
            {
                AugustTotalPaidIn = AugustPaidIn,
                AugustTotalWon = AugustWon,
                SeptemberTotalPaidIn = SepPaidIn,
                SeptemberTotalWon = SepWon,
                OctoberTotalPaidIn = OctPaidIn,
                OctoberTotalWon = OctWon,
                NovemberTotalPaidIn = NovPaidIn,
                NovemberTotalWon = NovWon,
                DecemberTotalPaidIn = DecPaidIn,
                DecemberTotalWon = DecWon,
                JanuaryTotalPaidIn = JanPaidIn,
                JanuaryTotalWon = JanWon,
                FebruaryTotalPaidIn = FebPaidIn,
                FebruaryTotalWon = FebWon,
                MarchTotalPaidIn = MarPaidIn,
                MarchTotalWon = MarWon,
                AprilTotalPaidIn = AprilPaidIn,
                AprilTotalWon = AprilWon,
                MayTotalPaidIn = MayPaidIn,
                MayTotalWon = MayWon


            };

            return newLine;
        }

        //create credit overview view model

        public CreditLineViewModel CreditLine(Guid Manager, bool Owed)
        {


            decimal AugustOwed = 0;
            decimal SepOwed = 0;
            decimal OctOwed = 0;
            decimal NovOwed = 0;
            decimal DecOwed = 0;
            decimal JanOwed = 0;
            decimal FebOwed = 0;
            decimal MarOwed = 0;
            decimal AprilOwed = 0;
            decimal MayOwed = 0;
            decimal TotalCredit = 0;

            List<Fees> feeList = db.Fees.ToList();
            List<Week> weekList = db.Week.ToList();

            AugustOwed = feeList.Where(x => x.FeeType == "Joining Fee").Select(p => p.FeeAmount).FirstOrDefault();
            SepOwed = feeList.Where(x => x.FeeType == "Cup Entry").Select(p => p.FeeAmount).FirstOrDefault();
            JanOwed = feeList.Where(x => x.FeeType == "League Cup Entry").Select(p => p.FeeAmount).FirstOrDefault();

            decimal weeklyFee = feeList.Where(x => x.FeeType == "Weekly").Select(p => p.FeeAmount).FirstOrDefault();

            foreach (var week in weekList)
            {
                if (week.WeekStartDate.Month == 8)
                {
                    AugustOwed = AugustOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 9)
                {
                    SepOwed = SepOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 10)
                {
                    OctOwed = OctOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 11)
                {
                    NovOwed = NovOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 12)
                {
                    DecOwed = DecOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 1)
                {
                    JanOwed = JanOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 2)
                {
                    FebOwed = FebOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 3)
                {
                    MarOwed = MarOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 4)
                {
                    AprilOwed = AprilOwed + weeklyFee;
                }
                else if (week.WeekStartDate.Month == 5)
                {
                    MayOwed = MayOwed + weeklyFee;
                }

            }
            decimal TotalOwedLocal = AugustOwed + SepOwed + OctOwed + NovOwed + DecOwed + JanOwed + FebOwed + MarOwed + AprilOwed + MayOwed;

            if (Owed == false)
            {
                List<Transaction> allTrans = db.Transaction.Where(x => x.Manager.ManagerId == Manager && x.TransactionType != "League or Cup" ).ToList();


                foreach (var trans in allTrans)
                {
                    TotalCredit = TotalCredit + trans.Value;
                }

                CreditLineViewModel newLine = new CreditLineViewModel
                {
                    AugustTotalOwed = AugustOwed,
                    AugustTotalCredit = TotalCredit - AugustOwed,
                    SeptemberTotalOwed = SepOwed,
                    SeptemberTotalCredit = TotalCredit - AugustOwed - SepOwed,
                    OctoberTotalOwed = OctOwed,
                    OctoberTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed,
                    NovemberTotalOwed = NovOwed,
                    NovemberTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed,
                    DecemberTotalOwed = DecOwed,
                    DecemberTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed,
                    JanuaryTotalOwed = JanOwed,
                    JanuaryTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed - JanOwed,
                    FebruaryTotalOwed = FebOwed,
                    FebruaryTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed - JanOwed - FebOwed,
                    MarchTotalOwed = MarOwed,
                    MarchTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed - JanOwed - FebOwed - MarOwed,
                    AprilTotalOwed = AprilOwed,
                    AprilTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed - JanOwed - FebOwed - MarOwed - AprilOwed,
                    MayTotalOwed = MayOwed,
                    MayTotalCredit = TotalCredit - AugustOwed - SepOwed - OctOwed - NovOwed - DecOwed - JanOwed - FebOwed - MarOwed - AprilOwed - MayOwed,
                    TotalOwed = AugustOwed + SepOwed + OctOwed + NovOwed + DecOwed + JanOwed + FebOwed + MarOwed + AprilOwed + MayOwed,
                    TotalCredits = TotalCredit - TotalOwedLocal
                };


                return newLine;
            }
            else
            {
                CreditLineViewModel newLine = new CreditLineViewModel
                {
                    AugustTotalOwed = AugustOwed,
                    SeptemberTotalOwed = SepOwed,
                    OctoberTotalOwed = OctOwed,
                    NovemberTotalOwed = NovOwed,
                    DecemberTotalOwed = DecOwed,
                    JanuaryTotalOwed = JanOwed,
                    FebruaryTotalOwed = FebOwed,
                    MarchTotalOwed = MarOwed,
                    AprilTotalOwed = AprilOwed,
                    MayTotalOwed = MayOwed,
                    TotalOwed = AugustOwed + SepOwed + OctOwed + NovOwed + DecOwed + JanOwed + FebOwed + MarOwed + AprilOwed + MayOwed

                };

                return newLine;
            }
        }


        //create balance overview view model
        public BalanceSheetViewModel BalanceOverview()
        {

            List<Fees> feeList = db.Fees.ToList();
            List<Prizes> prizeList = db.Prizes.ToList();
            List<Transaction> transactionList = db.Transaction.ToList();
            int weeks = db.Week.Count();
            int managers = db.Managers.Where(x => x.Active == true).Count();
            decimal weekFee = feeList.Where(x => x.FeeType == "Weekly").Select(p => p.FeeAmount).FirstOrDefault();
            decimal weekOut = 0;
            decimal joinFee = feeList.Where(x => x.FeeType == "Joining Fee").Select(p => p.FeeAmount).FirstOrDefault();
            decimal cupFee = feeList.Where(x => x.FeeType == "Cup Entry").Select(p => p.FeeAmount).FirstOrDefault();
            decimal lgCupFee = feeList.Where(x => x.FeeType == "League Cup Entry").Select(p => p.FeeAmount).FirstOrDefault();
            decimal fiver = prizeList.Where(x => x.PrizeType == "Five Fivers").Select(p => p.PrizeAmount).FirstOrDefault();
            decimal fiverOut = 0;
            decimal weeklyPrize = prizeList.Where(x => x.PrizeType == "Weekly Prize").Select(p => p.PrizeAmount).FirstOrDefault();
            decimal leaguecup = 0;
            decimal jackpotOut = 0;
            decimal leagueCupOut = 0;
            decimal currentTotalIn = 0;
            decimal jackpotCO = transactionList.Where(x => x.TransactionType == "Jackpot Carry Over").Select(x => x.Value).FirstOrDefault();
            

            foreach (var prize in prizeList.Where(x => x.LeaguePrize == true || x.CupPrize == true))
            {
                leaguecup = leaguecup + prize.PrizeAmount;
            }
            
            foreach(var tran in transactionList)
            {
                if (tran.TransactionType == "Weekly")
                {
                    weekOut = weekOut + tran.Value;
                }
                else if(tran.TransactionType == "Fiver")
                {
                    fiverOut = fiverOut + tran.Value;
                    currentTotalIn = currentTotalIn + tran.Value;
                }
                else if(tran.TransactionType == "Jackpot")
                {
                    jackpotOut = jackpotOut + tran.Value;
                    currentTotalIn = currentTotalIn + tran.Value;
                }
                else if(tran.TransactionType == "Ad-Hoc")
                {
                    currentTotalIn = currentTotalIn + tran.Value;
                }
                else
                {
                    leagueCupOut = leagueCupOut + tran.Value;
                }

            }

            BalanceSheetViewModel balanceVM = new BalanceSheetViewModel
            {
                WeeklyFees = weeks * managers * weekFee,
                JoiningFees = managers * joinFee,
                CupEntry = managers * cupFee,
                LeagueCupEntry = managers * lgCupFee,
                EndOfSeasonOut = leagueCupOut - jackpotCO,
                FiverDraws = fiver * 5 * 9,
                FiverDrawsOut = fiverOut,
                Jackpot = weeks * 2 + jackpotCO,
                JackpotCarryOver = jackpotCO,
                JackpotOut = jackpotOut,
                WeeklyPrize = weeks * weeklyPrize,
                WeeklyPrizeOut = weekOut,
                EndOfSeason = leaguecup,
                CurrentTotalIn = currentTotalIn + jackpotCO,
                CurrentTotalOut = leagueCupOut + fiverOut + weekOut + jackpotOut
            };

            balanceVM.TotalIn = balanceVM.WeeklyFees + balanceVM.JoiningFees + balanceVM.CupEntry + balanceVM.LeagueCupEntry + jackpotCO;
            balanceVM.TotalOut = balanceVM.FiverDraws + balanceVM.Jackpot + balanceVM.WeeklyPrize + balanceVM.EndOfSeason;


            return balanceVM;
        }

        public IndividualWinningsViewModel IndividualLine(Guid ManagerId)
        {

            List<Transaction> transList = db.Transaction.Where(x => x.ManagerId == ManagerId).ToList();
            List<Fees> feeList = db.Fees.ToList();

            decimal fives = 0;
            decimal week = 0;
            decimal jackpot = 0;
            decimal league = 0;
            decimal cup = 0;
            decimal total = 0;
            decimal payout = 0;
            decimal paidIn = 0;
            decimal owed = 0;

            foreach (var trans in transList)
            {
                if (trans.TransactionType == "Fiver")
                {
                    fives = fives + trans.Value;
                }
                else if (trans.TransactionType == "Weekly")
                {
                    week = week + trans.Value;
                }
                else if (trans.TransactionType == "Jackpot")
                {
                    jackpot = jackpot + trans.Value;
                }
                else if (trans.TransactionType == "League or Cup")
                {
                    if (trans.Notes == "Cup Win" || trans.Notes == "League Cup Win" || trans.Notes == "Cup Runner Up" || trans.Notes == "League Cup Runner Up")
                    {
                        cup = cup + trans.Value;
                    }
                    else
                    {
                        league = league + trans.Value;
                    }
                }

                else if (trans.TransactionType == "Ad-Hoc")
                {
                    paidIn = paidIn + trans.Value;
                }
            }

            foreach (var fee in feeList)
            {
                if (fee.FeeType == "Weekly")
                {
                    owed = owed + (fee.FeeAmount * db.Week.Count());
                }
                else
                {
                    owed = owed + fee.FeeAmount;
                }
            }

            total = fives + week + jackpot + league + cup;

            payout = total + paidIn - owed;


            IndividualWinningsViewModel individualWinnings = new IndividualWinningsViewModel
            {
                Fivers = fives,
                Weekly = week,
                Jackpot = jackpot,
                Cup = cup,
                League = league,
                Total = total,
                Payout = payout
            };

            return individualWinnings;
        }


        public FiversLineViewModel FiversMeetingLine(string Month)
        {
            var allTrans = db.Transaction.Where(x => x.TransactionType == "Fiver");
            List<string> mgrList = new List<string>();

            foreach (var trans in allTrans)
            {
                if (trans.TransactionDate.ToString("MMMM") == Month)
                {
                    mgrList.Add(trans.Manager.ManagerName);
                }
            }

            FiversLineViewModel newModel = new FiversLineViewModel
            {
                Manager = mgrList
            };

            return newModel;
        }

        public FiversManagerLine FiversManagerLineMethod(Manager manager)
        {
            var allTrans = db.Transaction.Where(x => x.TransactionType == "Fiver" && x.Manager.ManagerId == manager.ManagerId).ToList();

            decimal totalWon = 0;

            if (allTrans != null)
            {
                foreach (var trans in allTrans)
                {
                    totalWon = totalWon + trans.Value;
                }
            }
            

            FiversManagerLine newLine = new FiversManagerLine 
            {
               Manager = manager,
               Amount = totalWon
            };

            return newLine;

        }
}


}