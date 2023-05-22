using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngine.DocumentFormats;
using SearchEngine.Exceptions;
using System;
using System.IO;

namespace SearchEngineTests
{
    [TestClass]
    public class DocumentTests
    {
        private string docsPath = @"C:\Users\ENVY 15\source\repos\SearchEngine\Documents\";
        private string unauthorizedDocsPath = @"C:\Users\ENVY 15\source\repos\SearchEngine\Documents\UnauthorizedAccessException\";

        [TestMethod]
        public void Document_FileNotFound_ThrowsException()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new Word(new FileInfo(@"C:\file.docx")));
        }

        [TestMethod]
        public void WordDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new Word(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void PdfDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new Pdf(new FileInfo(docsPath + "CSC 302.docx")));
        }

        [TestMethod]
        public void ExcelDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new Excel(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void HtmlDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new Html(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void XmlDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new Xml(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void TextFileDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new TextFile(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void PowerPointDocument_InvalidFileType_ThrowException()
        {
            Assert.ThrowsException<InvalidFileTypeException>(() => new PowerPoint(new FileInfo(docsPath + "The_Matrix.pdf")));
        }

        [TestMethod]
        public void WordDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new Word(new FileInfo(unauthorizedDocsPath + "Books Android Application Project Report.docx")));
        }

        [TestMethod]
        public void PdfDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new Pdf(new FileInfo(unauthorizedDocsPath + "Assignment_1.pdf")));
        }

        [TestMethod]
        public void ExcelDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new Excel(new FileInfo(unauthorizedDocsPath + "Public-IRS_Category1_PeaceandSecurity.xlsx")));
        }

        [TestMethod]
        public void HtmlDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new Html(new FileInfo(unauthorizedDocsPath + "Bike sales down Good - I'm sick of the two-wheeled Stasi The Sun.html")));
        }

        [TestMethod]
        public void XmlDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new Xml(new FileInfo(unauthorizedDocsPath + "build.xml")));
        }

        [TestMethod]
        public void TextFileDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new TextFile(new FileInfo(unauthorizedDocsPath + "beautifyy.txt")));
        }

        [TestMethod]
        public void PowerPointDocument_UnauthorizedAccessException_ThrowException()
        {
            Assert.ThrowsException<System.UnauthorizedAccessException>(() => new PowerPoint(new FileInfo(unauthorizedDocsPath + "Concurrency with Java.pptx")));
        }

        [TestMethod]
        public void DocumentReplace_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => new Word(new FileInfo(docsPath + "CSC 302.docx")).Replace("old", "new"));
        }

        [TestMethod]
        public void DocumentAdd_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => new Word(new FileInfo(docsPath + "CSC 302.docx")).Add("word"));
        }

        [TestMethod]
        public void DocumentRemomve_UnparsedDocument_ThrowsException()
        {
            Assert.ThrowsException<UnparsedDocumentException>(() => new Word(new FileInfo(docsPath + "CSC 302.docx")).Remove("word"));
        }
    }
}
