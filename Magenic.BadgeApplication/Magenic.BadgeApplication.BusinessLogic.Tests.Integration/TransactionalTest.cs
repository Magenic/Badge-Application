using System;
using System.Transactions;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    public abstract class TransactionalTest 
    {
        protected void ExecuteWithTransaction(Action codeToRun)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                codeToRun.Invoke();
            }
        }
    }
}
