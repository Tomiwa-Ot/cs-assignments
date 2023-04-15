using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Representation of customers account
    /// </summary>
    public class Account
    {
        public string accountNumber { get; }
        public AccountType type { get; }
        public double balance { get; set; }
        public Customer customer { get; }
        public string transactionPin { get; set; }

        public Account(string accountNumber, Customer customer, AccountType type, string transactionPin)
        {
            this.accountNumber = accountNumber;
            this.customer = customer;
            this.type = type;
            this.transactionPin = transactionPin;
            balance = 0.00;
        }
    }
}
