using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BankApplication;

namespace BankApplicationTests
{
    
    [TestClass]
    public class UtilTests
    {
        private string password = "ignorant";
        private string hash = "1Wq5dIStZt6onQk0iogTs5tQCdo=:Cg8TAeMBMhE=";

        [TestMethod]
        public void HashString_GenerateHash_ReturnsSameString()
        {
            Assert.AreEqual(hash, Util.HashString(password));
        }

        [TestMethod]
        public void VerifyHash_ComparesHash_ReturnsTrue()
        {
            Assert.IsTrue(Util.VerifyHash(password, hash));
        }
    }
}
