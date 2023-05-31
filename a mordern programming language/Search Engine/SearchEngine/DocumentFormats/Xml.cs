using SearchEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SearchEngine.DocumentFormats
{
    /// <summary>
    /// Wrapper for xml documents
    /// </summary>
    public class Xml: Document
    {
        private XmlDocument xmlDocument;

        public Xml(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.xml };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);

            xmlDocument = new XmlDocument();
        }

        public override void Parse()
        {
            try
            {
                xmlDocument.Load(FilePath);
                XmlElement root = xmlDocument.DocumentElement;

                foreach (XmlNode node in root.ChildNodes)
                {
                    FileContent += node.Attributes["name"].Value + " ";
                }

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
