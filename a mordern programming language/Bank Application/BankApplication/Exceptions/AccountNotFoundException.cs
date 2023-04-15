using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Exception thrown if an account cannot be found in the data store
    /// </summary>
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() { }

        public AccountNotFoundException(string accountNumber)
            : base($"Account number: {accountNumber} not found")
        {

        }
    }
}
