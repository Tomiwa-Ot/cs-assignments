using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// All transactions that can be performed by bank application
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Deposits money into the customers account
        /// </summary>
        /// <param name="account">The account to be credited</param>
        /// <param name="amount">A non-negative and non-zero number to be deposited</param>
        public static void Deposit(Account account, double amount)
        {
            if(amount <= 0)
            {
                throw new InvalidAmountException(amount);
            }

            account.balance = account.balance + amount;
            Database.Update(account);
        }

        /// <summary>
        /// Withdraws money from a customers account
        /// </summary>
        /// <param name="account">The account to be debited</param>
        /// <param name="amount">A non-negative and non-zero number to be withdrawn</param>
        public static void Withdraw(Account account, double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException(amount);
            }

            if (account.balance < amount)
            {
                throw new InsufficientBalanceException(account.balance, amount);
            }

            account.balance = account.balance - amount;
            Database.Update(account);
        }

        /// <summary>
        /// Transfers money from one customer to another
        /// </summary>
        /// <param name="sender">The account to be debited</param>
        /// <param name="account">The account to be credited</param>
        /// <param name="amount">A non-negative and non-zero number to be transfered</param>
        public static void Transfer(Account sender, string account, double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException(amount);
            }

            if (sender.balance < amount)
            {
                throw new InsufficientBalanceException(sender.balance, amount);
            }

            Account recipient = Database.Search(account);
            if (recipient == null)
            {
                throw new AccountNotFoundException(account);
            }

            sender.balance = sender.balance - amount;
            recipient.balance = recipient.balance + amount;
            Database.Update(sender);
            Database.Update(recipient);
        }
    }
}
