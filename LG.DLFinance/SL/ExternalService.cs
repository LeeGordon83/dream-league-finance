using LG.DLFinance.DAL;


namespace LG.DLFinance.SL
{
    public static class ExternalService
    {
        public static void Update()
        {
            using (var db = new DLFinanceContext())
            {
                var transactionService = new TransactionService(db, new GeneralService());

                transactionService.WeeklyAPI();
            }
        }

        public static void Email()
        {
            using (var db = new DLFinanceContext())
            {
                var mailService = new MailService(db);

                mailService.SendCheck();
            }
        }


    }




}