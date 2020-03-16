using System.Collections.Generic;
using System.Web.Mvc;
using LG.DLFinance.ViewModels;

namespace LG.DLFinance.SL
{
    public interface IMailService
    {
        void Send(List<EmailViewModel> emailViewModels, ControllerContext context);
    }
}