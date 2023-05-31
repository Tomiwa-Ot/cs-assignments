using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    /// <summary>
    /// Unique indexed keywords
    /// </summary>
    public class InvertedIndex
    {
        public string Word { get; private set; }
        public List<DocumentWeight> Weights { get; set; }
        public int Frequency { get; set; }

        public InvertedIndex(string word, List<DocumentWeight> weight, int frequency)
        {
            Word = word;
            Weights = new List<DocumentWeight>(weight);
            Frequency = frequency;
        }
    }
}
