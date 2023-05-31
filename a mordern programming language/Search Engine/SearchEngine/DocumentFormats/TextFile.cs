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
    /// Wrapper for text documents
    /// </summary>
    public class TextFile: Document
    {
        public TextFile(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.txt };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);
        }

        public override void Parse()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    FileContent = sr.ReadToEnd();
                }

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
