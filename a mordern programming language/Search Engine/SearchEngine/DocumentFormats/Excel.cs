using ExcelDataReader;
using SearchEngine.Exceptions;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.DocumentFormats
{
    /// <summary>
    /// Wrapper for excel documents
    /// </summary>
    public class Excel: Document
    {
        public Excel(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.xls, DocumentType.xlsx };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);
        }

        public override void Parse()
        {
            try
            {
                using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Get the result set from the ExcelDataReader
                        var result = reader.AsDataSet();

                        // Get the DataTable for the first sheet in the result set
                        var dataTable = result.Tables[0];

                        // Iterate over all the rows in the DataTable
                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Iterate over all the columns in the row
                            foreach (DataColumn col in dataTable.Columns)
                            {
                                // Get the text value of the cell and add it to the StringBuilder
                                FileContent += row[col].ToString() + " ";
                            }
                        }
                    }
                }

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
