using GrapeCity.Documents.Pdf;
using SearchEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.DocumentFormats
{
    /// <summary>
    /// Wrapper for pdf documents
    /// </summary>
    public class Pdf : Document
    {
        private GcPdfDocument pdfDocument;

        public Pdf(FileInfo file) : base(file)
        {
            Types = new DocumentType[] { DocumentType.pdf };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);

            pdfDocument = new GcPdfDocument();
        }

        public override void Parse()
        {
            try
            {
                var file = File.OpenRead(FilePath);
                pdfDocument.Load(file);
                FileContent = pdfDocument.GetText();

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
