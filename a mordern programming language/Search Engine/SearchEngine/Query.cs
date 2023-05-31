using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    /// <summary>
    /// User search query
    /// </summary>
    public class Query: Document
    {
        public Query(string query): base(query) 
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException();
        }
    }
}
