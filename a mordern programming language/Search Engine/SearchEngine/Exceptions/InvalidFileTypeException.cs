using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Exceptions
{
    /// <summary>
    /// Exception for supplying documents with a wrong extension
    /// </summary>
    public class InvalidFileTypeException: Exception
    {
        public InvalidFileTypeException() { }

        public InvalidFileTypeException(DocumentType[] types, string extension)
            : base($"")
        {

        }
    }
}
