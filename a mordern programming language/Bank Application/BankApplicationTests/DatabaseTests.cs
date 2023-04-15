using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankApplication;

namespace BankApplicationTests
{
    [TestClass]
    public class DatabaseTests
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
        public void Update_AccountNotFound_ThrowsException()
        {
            Assert.ThrowsException<AccountNotFoundException>(() => Database.Update(account));
        }

        [TestMethod]
        public void Search_AccountNotFound_ReturnsNull()
        {
            Assert.IsNull(Database.Search(account.accountNumber));
        }
    }
}
