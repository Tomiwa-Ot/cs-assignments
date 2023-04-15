using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Exception thrown if customer enters a non-positive amount
    /// </summary>
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() { }

        public InvalidAmountException(double amount)
            : base($"Amount cannot be less than zero: {amount}")
        {

        }

    }
}
