using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Representation of a customer
    /// </summary>
    public class Customer
    {
        public string lastname { get; }
        public string firstname { get; }
        public string password { get; set; }

        public Customer(string lastname, string firstname, string password)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.password = password;
        }
    }
}
