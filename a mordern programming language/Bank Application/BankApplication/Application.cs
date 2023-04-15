using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Application functionalities, prompts and menus
    /// </summary>
    public class Application
    {
        private static Account account;        

        /// <summary>
        /// Starts bank application
        /// </summary>
        public static void Start()
        {
            Database.Initialize();
            while (true)
            {
                MainMenu();
            }
        }

        /// <summary>
        /// Displays bank logo
        /// </summary>
        private static void Banner()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(" ███████ ██    ██ ██████  ███████     ██████   █████  ███    ██ ██   ██ ");
            Console.WriteLine("    ███  ██    ██ ██   ██ ██          ██   ██ ██   ██ ████   ██ ██  ██  ");
            Console.WriteLine("   ███   ██    ██ ██████  █████       ██████  ███████ ██ ██  ██ █████   ");
            Console.WriteLine("  ███    ██    ██ ██   ██ ██          ██   ██ ██   ██ ██  ██ ██ ██  ██  ");
            Console.WriteLine(" ███████  ██████  ██████  ███████     ██████  ██   ██ ██   ████ ██   ██ ");
            Console.WriteLine("");
        }

        /// <summary>
        /// Menu displayed on application start
        /// </summary>
        private static void MainMenu()
        {
            Banner();
            Console.WriteLine("[1] Login\n[2] Create account\n[3] Exit\n");
            Console.Write(">: ");

            if (int.TryParse(Console.ReadLine(), out var input))
            {
                switch (input)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        CreateAccount();
                        break;
                    case 3:
                        Stop();
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }
            MainMenu();
        }

        /// <summary>
        /// Create an account for customer
        /// </summary>
        private static void CreateAccount()
        {
            Banner();
            Console.Write("[*] Enter Lastname: ");
            string lastname = Console.ReadLine();
            if(lastname == null || lastname.Length == 0)
            {
                DisplayErrorMessage("Lastname cannot be empty");
                MainMenu();
            }

            Console.Write("[*] Enter Firstname: ");
            string firstname = Console.ReadLine();
            if (firstname == null || firstname.Length == 0)
            {
                DisplayErrorMessage("Lastname cannot be empty");
                MainMenu();
            }

            Console.WriteLine("[*] Select account type: ");
            Console.WriteLine($" - 1. {AccountType.Savings}");
            Console.WriteLine($" - 2. {AccountType.Current}");
            Console.Write(">: ");
            if (!int.TryParse(Console.ReadLine(), out var accountType) && (accountType != 1 || accountType != 2))
            {
                DisplayErrorMessage("Invalid account type");
                MainMenu();
            }

            Console.Write("[*] Enter password: ");
            string password = Console.ReadLine();
            if(password == null || password.Length == 0)
            {
                DisplayErrorMessage("Password cannot empty");
                MainMenu();
            }

            Console.Write("[*] Enter 4 digit pin: ");
            string pin = Console.ReadLine();
            if (pin.Length != 4 && int.TryParse(pin, out var tmp))
            {
                DisplayErrorMessage("Invalid pin");
                MainMenu();
            }
            string accountNumber = Util.GenerateAccountNumber();

            Account newAccount = new Account(accountNumber, new Customer(lastname, firstname, Util.HashString(password)), (AccountType)accountType - 1, Util.HashString(pin));
            Database.Insert(newAccount);

            PrintCustomerData(newAccount);

            Console.Write("Press any key to continue...");
            Console.ReadLine();
            MainMenu();
        }

        /// <summary>
        /// Authenticate customer
        /// </summary>
        private static void Login()
        {
            Banner();
            Console.Write("[*] Enter account number: ");
            string accountNumber = Console.ReadLine();
            if(accountNumber == null || accountNumber.Length == 0)
            {
                DisplayErrorMessage("Account number cannot be empty");
                MainMenu();
            }

            Console.Write("[*] Enter password: ");
            string password = Console.ReadLine();
            if (password == null || password.Length == 0)
            {
                DisplayErrorMessage("Password cannot be empty");
                MainMenu();
            }

            Account result = Database.Search(accountNumber);
            if (result == null || !Util.VerifyHash(password, result.customer.password))
            {
                DisplayErrorMessage("AccountNo/Password incorrect");
                MainMenu();
            }
            account = result;
            CustomerMenu();
        }

        /// <summary>
        /// Menu displayed to authenticated customers
        /// </summary>
        private static void CustomerMenu()
        {
            Banner();
            PrintCustomerData(account);

            Console.WriteLine("[1] Transfer\n[2] Deposit\n[3] Withdraw\n[4] Reset pin\n[5] Reset password\n[6] Logout\n");
            Console.Write(">: ");

            if (int.TryParse(Console.ReadLine(), out var input))
            {
                switch (input)
                {
                    case 1:
                        Transfer();
                        break;
                    case 2:
                        Deposit();
                        break;
                    case 3:
                        Withdraw();
                        break;
                    case 4:
                        ResetTransactionPin();
                        break;
                    case 5:
                        ResetPassword();
                        break;
                    case 6:
                        Logout();
                        break;
                    default:
                        DisplayErrorMessage("Invalid choice...");
                        CustomerMenu();
                        break;
                }
            }
            CustomerMenu();
        }

        /// <summary>
        /// Prompt customer to make a deposit
        /// </summary>
        private static void Deposit()
        {
            Banner();
            if (!RequestForAmount(out var amount))
            {
                DisplayErrorMessage("Invalid amount...");
                CustomerMenu();
            }

            if (!ValidateTransactionPin())
            {
                DisplayErrorMessage("Incorrect pin...");
                CustomerMenu();
            }

            try
            {
                Transaction.Deposit(account, amount);
                Console.WriteLine($"\n[*] Successfully deposited #{amount}...");
                Console.ReadLine();
            }
            catch (InvalidAmountException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                CustomerMenu();
            }
        }

        /// <summary>
        /// Prompt customer to make a withdrawal
        /// </summary>
        private static void Withdraw()
        {
            Banner();
            if (!RequestForAmount(out var amount))
            {
                DisplayErrorMessage("Invalid amount...");
                CustomerMenu();
            }

            if (!ValidateTransactionPin())
            {
                DisplayErrorMessage("Incorrect pin...");
                CustomerMenu();
            }

            try
            {
                Transaction.Withdraw(account, amount);
                Console.WriteLine($"\n[*] Successfully withdrawn #{amount}...");
                Console.ReadLine();
            }
            catch (InvalidAmountException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                CustomerMenu();
            }
        }

        /// <summary>
        /// Prompt customer to make a transfer
        /// </summary>
        private static void Transfer()
        {
            Banner();
            Console.Write("[*] Enter recipient account number: ");
            string recipient = Console.ReadLine();

            if(!RequestForAmount(out var amount))
            {
                DisplayErrorMessage("Invalid amount...");
                CustomerMenu();
            }

            if (!ValidateTransactionPin())
            {
                DisplayErrorMessage("Incorrect pin...");
                CustomerMenu();
            }

            try
            {
                Transaction.Transfer(account, recipient, amount);
                Console.WriteLine($"\n[*] Successfully transfered #{amount} to {recipient}...");
                Console.ReadLine();
            }
            catch (AccountNotFoundException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            catch (InvalidAmountException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                CustomerMenu();
            }
        }

        /// <summary>
        /// Deauthenticate the customer from the application
        /// </summary>
        private static void Logout()
        {
            account = null;
            MainMenu();
        }

        /// <summary>
        /// Prompt the customer to enter an amount
        /// </summary>
        /// <param name="amount">Reference variable to store inouted amount</param>
        /// <returns><c>true</c> if the user enters a numeric amount; else <c>false</c></returns>
        private static bool RequestForAmount(out double amount)
        {
            Console.Write("[*] Enter amount: ");
            return double.TryParse(Console.ReadLine(), out amount);
        }

        /// <summary>
        /// Checks if the supplied pin matches that in the customers record
        /// </summary>
        /// <returns></returns>
        private static bool ValidateTransactionPin()
        {
            Console.Write("[*] Enter transaction pin: ");
            string pin = Console.ReadLine();
            return int.TryParse(pin, out var tmp) && Util.VerifyHash(pin, account.transactionPin);
        }

        /// <summary>
        /// Reset the logged in customers transaction pin
        /// </summary>
        private static void ResetTransactionPin()
        {
            Banner();
            Console.Write("[*] New 4 digit pin: ");
            string newPin = Console.ReadLine();
            if(newPin.Length != 4 && !int.TryParse(newPin, out var tmp))
            {
                DisplayErrorMessage("Invalid format");
                return;
            }

            Console.Write("[*] Confirm 4 digit pin: ");
            string confirmPin = Console.ReadLine();
            if (confirmPin.Length != 4 && !int.TryParse(confirmPin, out var tmp1))
            {
                DisplayErrorMessage("Invalid format");
                return;
            }

            if (newPin == confirmPin)
            {
                account.transactionPin = Util.HashString(newPin);
                Database.Update(account);
                Console.WriteLine("\n[*] Pin reset complete...");
                Console.ReadLine();
            } else
            {
                DisplayErrorMessage("Pins do not match");
            }
            CustomerMenu();
            
        }

        /// <summary>
        /// Reset the logged in customers password
        /// </summary>
        private static void ResetPassword()
        {
            Banner();
            Console.Write("[*] New password: ");
            string newPassword = Console.ReadLine();
            if(newPassword == null || newPassword.Length == 0)
            {
                DisplayErrorMessage("Password cannot be empty");
                return;
            }

            Console.Write("[*] Confirm password: ");
            string confirmPassword = Console.ReadLine();
            if (confirmPassword == null || confirmPassword.Length == 0)
            {
                DisplayErrorMessage("Password cannot be empty");
                return;
            }

            if (newPassword == confirmPassword)
            {
                account.customer.password = Util.HashString(newPassword);
                Database.Update(account);
                Console.WriteLine("\n[*] Password reset complete...");
                Console.ReadLine();
            }
            else
            {
                DisplayErrorMessage("Passwords do not match");
            }
            CustomerMenu();
        }

        /// <summary>
        /// Displays an error response in the console
        /// </summary>
        /// <param name="message">The error message to be displayed</param>
        private static void DisplayErrorMessage(string message)
        {
            Console.WriteLine($"\n[!] {message}");
            Console.ReadLine();
        }

        /// <summary>
        /// Display logged in customers details
        /// </summary>
        /// <param name="account"></param>
        private static void PrintCustomerData(Account account)
        {
            Console.WriteLine($"\n Name: {account.customer.lastname} {account.customer.firstname}");
            Console.WriteLine($" AccountNo: {account.accountNumber}");
            Console.WriteLine($" Account Type: {Enum.GetName(typeof(AccountType), account.type)}");
            Console.WriteLine($" Balance: # {account.balance}\n");
        }

        /// <summary>
        /// Stops bank application
        /// </summary>
        private static void Stop()
        {
            Environment.Exit(0);
        }
    }
}
