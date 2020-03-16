using System.Collections.Generic;
using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;

namespace LG.DLFinance.SL
{
    public interface IAdminService
    {
        List<Emails> MapEmails(ManagerViewModel manager, bool isEdit);

        void LogHistory();

        EmailViewModel GetEmailData(Manager manager);
    }
}