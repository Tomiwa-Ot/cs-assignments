using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngine;
using SearchEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngineTests
{
    /// <summary>
    /// Summary description for QueryTests
    /// </summary>
    [TestClass]
    public class QueryTests
    {
        private Query query = new Query("test");

        [TestMethod]
        public void Query_ArgumentNull_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Query(""));
        }

        [TestMethod]
        public void QueryCreationTime_UnsupportedMethod_ThrowsException()
        {
            Assert.ThrowsException<UnsupportedMethodException>(() => query.CreationTime());
        }

        [TestMethod]
        public void QueryLastModified_UnsupportedMethod_ThrowsException()
        {
            Assert.ThrowsException<UnsupportedMethodException>(() => query.LastModified());
        }

        [TestMethod]
        public void QuerySize_UnsupportedMethod_ThrowsException()
        {
            Assert.ThrowsException<UnsupportedMethodException>(() => query.Size());
        }

        [TestMethod]
        public void QueryReplace_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => query.Replace("old", "new"));
        }

        [TestMethod]
        public void QueryAdd_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => query.Add("word"));
        }

        [TestMethod]
        public void QueryRemomve_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => query.Remove("word"));
        }
    }
}
