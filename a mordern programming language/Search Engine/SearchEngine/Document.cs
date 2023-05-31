using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Util;
using Lucene.Net.Analysis.En;
using StopWord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Security.Principal;
using SearchEngine.Exceptions;

namespace SearchEngine
{
    /// <summary>
    /// Wrapper for parsing documents and queries
    /// </summary>
    public abstract class Document
    {
        public string query { get; protected set; }
        public DocumentType[] Types { get; protected set; }
        public List<string> Keywords { get; protected set; }
        public Dictionary<string, int> Index { get; private set; }

        private FileInfo file;
        public string FilePath { get; private set; }
        public string Filename { get; private set; }
        public string FileExtension { get; private set; }
        public string FileContent { get; protected set; }

        public Document(FileInfo file)
        {
            this.file = file;
            FilePath = file.FullName;
            Filename = file.Name;
            FileExtension = file.Extension;
            Keywords = new List<string>();
            Index = new Dictionary<string, int>();
            

            if (!File.Exists(FilePath))
                throw new FileNotFoundException();

            if (!CanFileBeRead(FilePath))
                throw new System.UnauthorizedAccessException();
        }

        public Document(string query)
        {
            this.query = query;
            Keywords = new List<string>();
            Index = new Dictionary<string, int>();
        }

        /// <summary>
        /// Generate indexed keywords
        /// </summary>
        public virtual void Parse()
        {
            if (string.IsNullOrEmpty(FileContent))
                Tokenize(query);
            else
                Tokenize(FileContent);
            RemoveStopWords();
            StemKeywords();
            IndexKeywords();
        }

        /// <summary>
        /// Extracts words from documents
        /// </summary>
        private void Tokenize(string text)
        {
            Regex regex = new Regex(@"\b\w+\b|[^\w\s]");

            MatchCollection matches = regex.Matches(text);

            for (int i = 0; i < matches.Count; i++)
            { 
                if (matches[i].Value.All((s) => Char.IsLetter(s)))
                    Keywords.Add(matches[i].Value.ToLower());
            }
        }

        /// <summary>
        /// Removes stopwords from the documents keywords
        /// </summary>
        private void RemoveStopWords()
        {
            var stopWords = StopWords.GetStopWords("en");
            var text = new List<string>(Keywords);

            foreach (string word in text)
            {
                if (stopWords.Contains(word)) Keywords.Remove(word);
            }
        }

        /// <summary>
        /// Remove affixes from base word
        /// </summary>
        private void StemKeywords()
        {
            const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
            var stemmer = new EnglishAnalyzer(AppLuceneVersion);
            var words = new List<string>();
            Keywords.ForEach(word =>
            {
                var tokenStream = stemmer.GetTokenStream(null, new StringReader(word));
                var stemmedText = new StringBuilder();
                var termAttr = tokenStream.GetAttribute<ICharTermAttribute>();
                tokenStream.Reset();
                while (tokenStream.IncrementToken())
                {
                    stemmedText.Append(termAttr.ToString());
                }
                words.Add(word);
                tokenStream.Reset();
            });

            Keywords = words;
            
        }

        /// <summary>
        /// Computes the frequency for each unique keyword
        /// </summary>
        private void IndexKeywords()
        {
            foreach (string word in Keywords)
            {
                if (Index.ContainsKey(word))
                    Index[word]++;
                else
                    Index[word] = 1;
            }
        }

        /// <summary>
        /// Checks if the supplied file passes the extension criteria
        /// </summary>
        /// <returns><c>True</c> if extensions match; false otherwise</returns>
        protected bool IsFileExtensionAllowed()
        {
            foreach(DocumentType type in Types)
            {
                if (Enum.GetName(typeof(DocumentType), type).Equals(FileExtension.ToLower().Replace('.', ' ').Trim())) return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if current user has permission to read a file
        /// </summary>
        /// <param name="path">The file to be checked for read permission</param>
        /// <returns><c>True</c> if the current user has permission to read the file;<c>false</c> otherwise</returns>
        private bool CanFileBeRead(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether a word is part of the document keywords
        /// </summary>
        /// <param name="word">The word to search for</param>
        /// <returns><c>True</c> if the word is found; otherwise <c>false</c></returns>
        public bool Contains(string word)
        {
            if (Keywords.Count == 0)
                throw new UnparsedDocumentException(GetType().Name);

            return Keywords.Contains(word);
        }

        /// <summary>
        /// Returns the last time the document was modified
        /// </summary>
        /// <returns>The date and time the file was last modified</returns>
        public DateTime LastModified()
        {
            if (!string.IsNullOrEmpty(query))
                throw new UnsupportedMethodException(this.GetType().Name);

            return file.LastWriteTime;
        }

        /// <summary>
        /// Returns the document creation date
        /// </summary>
        /// <returns>The date and time the file was created</returns>
        public DateTime CreationTime()
        {
            if (!string.IsNullOrEmpty(query))
                throw new UnsupportedMethodException(GetType().Name);

            return file.CreationTime;
        }

        /// <summary>
        /// Returns the document size
        /// </summary>
        /// <returns>The size of the current file in bytes</returns>
        public long Size()
        {
            if (!string.IsNullOrEmpty(query))
                throw new UnsupportedMethodException(GetType().Name);

            return file.Length;
        }

        /// <summary>
        /// Replace the first occurence of a word in the documents keywords
        /// </summary>
        /// <param name="find">The word to be replaced</param>
        /// <param name="replacement">The new word to be added</param>
        public void Replace(string find, string replacement)
        {
            if (Keywords.Count == 0)
                throw new UnparsedDocumentException(GetType().Name);

            Keywords = Keywords.Select((word, index) => index == Keywords.IndexOf(find) ? replacement : word).ToList();
        }

        /// <summary>
        /// Add a word into the documents keywords
        /// </summary>
        /// <param name="word">The word to be added to the keywords</param>
        public void Add(string word)
        {
            if (Keywords.Count == 0)
                throw new UnparsedDocumentException(GetType().Name);

            Keywords.Add(word);
        }

        /// <summary>
        /// Removes the first occurence of a word in the documents keywords
        /// </summary>
        /// <param name="word">The word to be removed</param>
        public void Remove(string word)
        {
            if (Keywords.Count == 0)
                throw new UnparsedDocumentException(GetType().Name);

            Keywords.Remove(word);
        }

        /// <summary>
        /// String representation of the document
        /// </summary>
        /// <returns>The documents keywords</returns>
        public override string ToString()
        {
            return string.Join(", ", Keywords);
        }
    }
}
