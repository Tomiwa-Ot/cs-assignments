using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Exception thrown if the transaction amount is greater than the customers balance
    /// </summary>
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() { }

        public InsufficientBalanceException(double balance, double amount)
            : base(String.Format($"Insufficient balance: {balance}, amount: {amount} "))
        {

        }
    }
}
