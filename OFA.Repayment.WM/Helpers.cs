using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM
{
    public static class Helpers
    {
        public static string ExceptionTemplate
            => "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}";
    }
}
