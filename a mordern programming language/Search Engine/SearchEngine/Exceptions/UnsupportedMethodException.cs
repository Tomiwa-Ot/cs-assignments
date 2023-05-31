using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Exceptions
{
    /// <summary>
    /// Exception called when a method is not supported
    /// </summary>
    public class UnsupportedMethodException: Exception
    {
        public UnsupportedMethodException() { }

        public UnsupportedMethodException(string className)
            : base($"{className} doesn't support the called method")
        {
            
        }
    }
}
