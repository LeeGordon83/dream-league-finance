using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;

namespace LG.DLFinance.SL
{
    public interface ITransactionService
    {
        void AdhocProcess(PaymentViewModel payment);
        void FiverProcess(SmallPrizeListViewModel fiverViewModels);
        void WeeklyProcess(SmallPrizeListViewModel weeklyViewModels);
        void JackpotProcess(PaymentViewModel payment);
        void EndCurrentJackpot(Jackpot currentJackpot);
        void WeeklyAPI();
        void ChangedTransaction(Transaction transaction, bool delete);
        void LgCupProcess(LgCupWinnersViewModel LeagueCup);
        void JackpotAdd();
        void JackpotCarryOver(PaymentViewModel payment);
    }
}