using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Exceptions
{
    public class UnparsedDocumentException: Exception
    {
        public UnparsedDocumentException() { }

        public UnparsedDocumentException(string className)
            : base($"{className} document has not been parsed")
        {

        }
    }
}
