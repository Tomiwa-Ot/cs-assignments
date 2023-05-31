using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SearchEngine.Exceptions;
using System;
using System.IO;

namespace SearchEngine.DocumentFormats
{
    /// <summary>
    /// Wrapper for word documents
    /// </summary>
    public class Word: Document
    {
        public Word(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.doc, DocumentType.docx };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);
        }

        public override void Parse()
        {
            try
            {
                using (WordprocessingDocument document = WordprocessingDocument.Open(FilePath, false))
                {
                    MainDocumentPart mainPart = document.MainDocumentPart;
                    Body body = mainPart.Document.Body;
                    FileContent += body.InnerText;
                }

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
