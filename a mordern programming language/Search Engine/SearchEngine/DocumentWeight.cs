using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    /// <summary>
    /// Wrapper for a keywords score in a document
    /// </summary>
    public class DocumentWeight
    {
        public string Id { get; private set; }
        public int TermFrequency { get; private set; }
        public int KeywordCount { get; private set; }
        public double Score { get; set; }

        public DocumentWeight(string id, int termFrequency, int keywordCount)
        {
            Id = id;
            TermFrequency = termFrequency;
            KeywordCount = keywordCount;
            Score = 0;
        }

        /// <summary>
        /// Calculates degree of relevance of a document to a query
        /// </summary>
        /// <param name="numberOfDocsInRespository"></param>
        /// <param name="numberOfDocsWithTerm"></param>
        public void Tf_IDF(int numberOfDocsInRespository, int numberOfDocsWithTerm)
        {
            double tf = (double) TermFrequency /  (double) KeywordCount;
            double idf = Math.Log(numberOfDocsInRespository / numberOfDocsWithTerm, 2);
            Score += tf * idf;
        }
    }
}
