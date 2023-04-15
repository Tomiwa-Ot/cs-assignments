using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using System;

namespace BankApplication
{
    /// <summary>
    /// Simulation of a database system
    /// </summary>
    public class Database
    {
        private static string databaseFile = "data.json";
        private static string path = $"C:\\Users\\ENVY 15\\source\\repos\\BankApplication\\{databaseFile}";
        public static List<Account> accounts = new List<Account>();

        /// <summary>
        /// Loads account information from data store
        /// </summary>
        public static void Initialize()
        {
            string json = File.ReadAllText(path);
            accounts = JsonSerializer.Deserialize<List<Account>>(json);
        }

        /// <summary>
        /// Adds a new account into the account list
        /// </summary>
        /// <param name="account"></param>
        public static void Insert(Account account)
        {
            accounts.Add(account);
            Commit();
        }

        /// <summary>
        /// Update data about an existing account
        /// </summary>
        /// <param name="account"></param>
        public static void Update(Account account)
        {
            Account searchResult = Search(account.accountNumber);
            if (searchResult == null)
            {
                throw new AccountNotFoundException(account.accountNumber);
            }
            searchResult = account;
            Commit();
        }

        /// <summary>
        /// Searches for an account in the data store
        /// </summary>
        /// <param name="accountNumber">Account to search for</param>
        /// <returns>If the account exists, it is returned; else <c>null</c></returns>
        public static Account Search(string accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (account.accountNumber.Equals(accountNumber))
                {
                    return account;
                }
            }
            return null;
        }

        /// <summary>
        /// Publish the account list to the data store
        /// </summary>
        public static void Commit()
        {
            string json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(path, json);
        }
    }
}
