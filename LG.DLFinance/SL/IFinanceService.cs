using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;
using System;
using System.Collections.Generic;

namespace LG.DLFinance.SL
{
    public interface IFinanceService
    {
        PaidInViewModel PaidIn();

        PaidInLineViewModel PaidInLine(Guid Manager);

        BalanceSheetViewModel BalanceOverview();

        CreditLineViewModel CreditLine(Guid Manager, bool Owed);

        IndividualWinningsViewModel IndividualLine(Guid ManagerId);

        FiversLineViewModel FiversMeetingLine(string Month);

        FiversManagerLine FiversManagerLineMethod(Manager manager);
    }


}