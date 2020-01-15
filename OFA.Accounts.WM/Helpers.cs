using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM
{
    public static class Helpers
    {
        public static int CalculateRunningBalance(int debitAmount, int creditAmountAmount, int runningBalance)
        {
            if (debitAmount > 0)
                runningBalance -= debitAmount;
            if (creditAmountAmount > 0)
                runningBalance += creditAmountAmount;

            return runningBalance;
        }
    }

    public enum AccountStatus
    {
        PENDING = 0,
        REPAID = 1,
        DEFAULTED
    }
}
