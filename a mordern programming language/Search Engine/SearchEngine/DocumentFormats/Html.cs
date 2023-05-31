using HtmlAgilityPack;
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
    /// Wrapper for html documents
    /// </summary>
    public class Html: Document
    {
        private HtmlDocument htmlDocument;

        public Html(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.html };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);

            htmlDocument = new HtmlDocument();
        }

        public override void Parse()
        {
            try
            {
                htmlDocument.Load(FilePath);

                var bodyNode = htmlDocument.DocumentNode.SelectSingleNode("//body");
                FileContent = bodyNode.InnerText;

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
