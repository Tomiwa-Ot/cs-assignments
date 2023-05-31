using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using MySql.Data.MySqlClient;
using SearchEngine.DocumentFormats; 

namespace SearchEngine
{
    /// <summary>
    /// Search engine indexing, ranking and matching function
    /// </summary>
    public class Engine
    {
        private static List<InvertedIndex> Indexes = new List<InvertedIndex>();
        private static MySql.Data.MySqlClient.MySqlConnection connection;

        public static string DocumentsPath = @"C:\Users\ENVY 15\source\repos\SearchEngine\Documents\";
        private static string ConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=search_engine";

        public static void Main(string[] args)
        {
            
        }

    /// <summary>
    /// Establish connection to database
    /// </summary>
    private static void ConnectToDatabase()
        {
            try 
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException) {}
        }

        /// <summary>
        /// Close connection to database
        /// </summary>
        private static void DisconnectFromDatabase()
        {
            connection.Close();
        }

        /// <summary>
        /// Map query to relevant documents
        /// </summary>
        /// <param name="query">The query to find matches for</param>
        public static string FindMatch(Query query)
        {
            if (query == null)
                throw new ArgumentNullException();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                query.Parse();

                ConnectToDatabase();
                string sqlQuery = "SELECT * FROM inverted_index WHERE word=@word LIMIT 1";
                List<InvertedIndex> matches = new List<InvertedIndex>();

                foreach (string word in query.Keywords)
                {
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@word", word);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Retrieve the values from the selected rows
                        string term = reader.GetString("word");
                        int frequency = reader.GetInt32("frequency");
                        List<DocumentWeight> weights = JsonSerializer.Deserialize<List<DocumentWeight>>(reader.GetString("document_id"));

                        matches.Add(new InvertedIndex(term, weights, frequency));
                    }

                    // Close the data reader and connection objects
                    reader.Close();
                }

                DisconnectFromDatabase();

                int numberOfDocuments = Directory.GetFiles(DocumentsPath, "*.*", SearchOption.AllDirectories).Length;
                return RankDocuments(matches, query, numberOfDocuments, stopwatch);
            }
            catch (Exception exception)
            {
                return JsonSerializer.Serialize(exception.ToString());
            }
        }

        /// <summary>
        /// Indexes documents in search engine repository
        /// </summary>
        public static void IndexDocuments()
        {
            string[] files = Directory.GetFiles(DocumentsPath, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                byte[] fileNameInBytes = Encoding.UTF8.GetBytes(file);
                ParseDocument(new FileInfo(file), out Document document);
                
                if (document != null)
                {
                    foreach (KeyValuePair<string, int> entry in document.Index)
                    {
                        bool isWordPartOfIndex = false;
                        foreach (InvertedIndex index in Indexes)
                        {
                            if (index.Word.Equals(entry.Key))
                            {
                                index.Frequency += entry.Value;
                                index.Weights.Add(new DocumentWeight(Convert.ToBase64String(fileNameInBytes), entry.Value, document.Index.Count));
                                isWordPartOfIndex = true;
                                break;
                            }
                        }

                        if (!isWordPartOfIndex)
                        {
                            Indexes.Add(new InvertedIndex(entry.Key, new List<DocumentWeight>() { new DocumentWeight(Convert.ToBase64String(fileNameInBytes), entry.Value, document.Index.Count) }, entry.Value));
                        }
                    }
                }
            }
            Indexes = Indexes.OrderBy(index => index.Word).ToList();

            try
            {
                ConnectToDatabase();
                DeleteInvertedIndexesFromDatabase();
                PopulateInvertedIndexTable();
                DisconnectFromDatabase();
            } catch (Exception) { return; }
        }

        /// <summary>
        /// Generate and index keywords from the documents in the repository
        /// </summary>
        /// <param name="file">The file to be parsed</param>
        /// <param name="document">Document object representation</param>
        private static void ParseDocument(FileInfo file, out Document document)
        {
            try
            {
                if (Enum.TryParse<DocumentType>(file.Extension.Replace('.', ' ').Trim(), out DocumentType documentType))
                {
                    switch (documentType)
                    {
                        case DocumentType.doc:
                            document = new Word(file);
                            break;
                        case DocumentType.docx:
                            document = new Word(file);
                            break;
                        case DocumentType.html:
                            document = new Html(file);
                            break;
                        case DocumentType.pdf:
                            document = new Pdf(file);
                            break;
                        case DocumentType.ppt:
                            document = new PowerPoint(file);
                            break;
                        case DocumentType.pptx:
                            document = new PowerPoint(file);
                            break;
                        case DocumentType.txt:
                            document = new TextFile(file);
                            break;
                        case DocumentType.xls:
                            document = new Excel(file);
                            break;
                        case DocumentType.xlsx:
                            document = new Excel(file);
                            break;
                        case DocumentType.xml:
                            document = new Xml(file);
                            break;
                        default:
                            document = null;
                            return;
                    }
                }
                else
                {
                    document = null;
                    return;
                }

                document.Parse();
            }
            catch (Exception) 
            {
                document = null;
                return;
            }
        }

        /// <summary>
        /// Rank documents related to query based on relevance using TF-IDF
        /// </summary>
        /// <param name="rows">The relevant records found in the database</param>
        /// <param name="query">The query supplied by the user</param>
        /// <param name="numberOfDocuments">Count of the number of documents in the repository</param>
        /// <returns>A Json array of documents ranked in decreasing relevancy</returns>
        private static string RankDocuments(List<InvertedIndex> rows, Query query, int numberOfDocuments, Stopwatch stopwatch)
        {
            List<DocumentWeight> scoredWeights = new List<DocumentWeight>();
            foreach (InvertedIndex row in rows)
            {
                foreach (DocumentWeight weight in row.Weights)
                {
                    if (!scoredWeights.Contains(weight)) scoredWeights.Add(weight);
                }
            }

            foreach (string word in query.Keywords)
            {
                foreach (DocumentWeight weight in scoredWeights)
                {
                    try
                    {
                        weight.Tf_IDF(numberOfDocuments, rows.FirstOrDefault(x => x.Word == word).Weights.Count);
                    } catch (Exception) { continue; }
                }
            }

            var sortedDocumentScores = scoredWeights.OrderByDescending(x => x.Score).ToList<DocumentWeight>();
            var documentNames = new List<string>();
            foreach (DocumentWeight weight in sortedDocumentScores)
            {
                byte[] data = Convert.FromBase64String(weight.Id);
                FileInfo file = new FileInfo(Encoding.UTF8.GetString(data));
                int startIndex = file.FullName.IndexOf(@"\Documents\") + @"\Documents\".Length;
                documentNames.Add(file.FullName.Substring(startIndex));
            }

            stopwatch.Stop();
            return JsonSerializer.Serialize(new Dictionary<string, object>
                {
                    { "results", documentNames },
                    { "time", stopwatch.Elapsed.TotalSeconds }
                }
            );
        }

        /// <summary>
        /// Insert newly generated inverted indexes into database
        /// </summary>
        private static void PopulateInvertedIndexTable()
        {
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (InvertedIndex index in Indexes)
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO inverted_index(word, frequency, document_id) VALUES(@word, @frequency, @document_id)", connection);
                        command.Parameters.AddWithValue("@word", index.Word);
                        command.Parameters.AddWithValue("@frequency", index.Frequency);
                        command.Parameters.AddWithValue("@document_id", JsonSerializer.Serialize(index.Weights));

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        /// <summary>
        /// Delete inverted indexes stored in the database
        /// </summary>
        private static void DeleteInvertedIndexesFromDatabase()
        {
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string command = "DELETE FROM inverted_index";

                    MySqlCommand sqlCommand = new MySqlCommand(command, connection);
                    sqlCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
