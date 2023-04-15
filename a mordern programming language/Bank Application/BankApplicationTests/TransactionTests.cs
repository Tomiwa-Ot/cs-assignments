using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BankApplication;

namespace BankApplicationTests
{
    [TestClass]
    public class TransactionTests
    {
        private Account account;

        [TestInitialize]
        public void Initialize()
        {
            string accountNumber = "1234567890";
            Customer customer = new Customer("Jackson", "Michael", "password-hash");
            account = new Account(accountNumber, customer, AccountType.Savings, "pin-hash");
        }

        [TestMethod]
        public void Deposit_NonPositiveAmount_ThrowsException()
        {
            double amount = -132.00;
            Assert.ThrowsException<InvalidAmountException>(() => Transaction.Deposit(account, amount));
        }

        [TestMethod]
        public void Withdraw_NonPositiveAmount_ThrowsException()
        {
            double amount = -132.00;
            Assert.ThrowsException<InvalidAmountException>(() => Transaction.Withdraw(account, amount));
        }

        [TestMethod]
        public void Withdraw_InsufficientBalance_ThrowsException()
        {
            double amount = 132.00;
            Assert.ThrowsException<InsufficientBalanceException>(() => Transaction.Withdraw(account, amount));
        }

        [TestMethod]
        public void Transfer_NonPositiveAmount_ThrowsException()
        {
            double amount = -132.00;
            string recipient = "0987654321";
            Assert.ThrowsException<InvalidAmountException>(() => Transaction.Transfer(account, recipient, amount));
        }

        [TestMethod]
        public void Transfer_InsufficientBalance_ThrowsException()
        {
            double amount = 132.00;
            string recipient = "0987654321";
            Assert.ThrowsException<InsufficientBalanceException>(() => Transaction.Transfer(account, recipient, amount));
        }

        [TestMethod]
        public void Transfer_AccountNotFound_ThrowsException()
        {
            double amount = 132.00;
            string recipient = "0987654321";
            account.balance = 500.00;
            Assert.ThrowsException<AccountNotFoundException>(() => Transaction.Transfer(account, recipient, amount));
        }
    }
}
