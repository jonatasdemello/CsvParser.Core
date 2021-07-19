using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CsvParser.Contracts;
using CsvParser.Domain;
using Microsoft.VisualBasic.FileIO;

namespace CsvParser.Services
{
    /// <summary>
    /// This file must be unit tested.
    /// </summary>
    public class ParsingService : IParsingService
    {
        /// <summary>
        /// Accepts a string with the contents of the csv file in it and should return a parsed csv file.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        public CsvTable ParseCsv(string fileContent, bool containsHeader)
        {
            //TODO fill in your logic here
            //throw new NotImplementedException();

            // now parse string fileContent in CsvTable object
            var result = new CsvTable();
            var rowsArr = ConvertToLines(fileContent);

            // header first
            var HeaderRow = new CsvRow();
            if (containsHeader)
            {
                HeaderRow = ConvertLinetoRow(rowsArr[0]);
            }
            result.HeaderRow = HeaderRow;
            // table rows
            var DataRows = new List<CsvRow>();
            if (rowsArr.Length > 0)
            {
                for (var i = (containsHeader ? 1 : 0); i < rowsArr.Length; i++)
                {
                    DataRows.Add(ConvertLinetoRow(rowsArr[i]));
                }
            }
            result.Rows = DataRows;

            return result;
        }

        // helper methods, here for now
        private string[] ConvertToLines(string text)
        {
            return text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        // helper methods, here for now
        private CsvRow ConvertLinetoRow(string text)
        {
            // assuming a perfect formated csv
            var dataRow = new CsvRow();
            var rowCols = text.Split(',');
            for (var j = 0; j < rowCols.Length; j++)
            {
                var col = new CsvColumn(rowCols[j]);
                dataRow.Columns.Add(col);
            }
            return dataRow;
        }

        //================================================================
        // There is allways another way to do it....
        //================================================================

        public CsvTable ParseCsv_v1(string fileContent, bool containsHeader)
        {
            try
            {
                var table = new CsvTable();

                string[] lines = fileContent.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                for (int i = 0; i < lines.Length; i++)
                {
                    var row = lines[i];

                    if (containsHeader && i == 0)
                    {
                        table.HeaderRow = ParseRow_v1(row);
                    }
                    else
                    {
                        table.Rows.Add(ParseRow_v1(row));
                    }
                }

                return table;
            }
            catch
            {
                // Row parsing failures should bubble up to here.
                return null;
            }
        }

        /// <summary>
        /// Accepts a single-line string with the contents of the CSV row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public CsvRow ParseRow_v1(string line)
        {
            // As per http://edoceo.com/utilitas/csv-file-format, leading and trailing whitespace should be ignored.
            // Also need to allow double-quotes and commas within quotes.
            const char escape = '"';
            const char split = ',';
            bool escapedPrior = false;
            bool escaped = false;

            line = line.Trim();

            var row = new CsvRow();
            var sb = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                switch (c)
                {
                    case escape:
                        if (!escaped)
                        {
                            escaped = true;
                        }
                        else
                        {
                            if (!escapedPrior)
                            {
                                if ((i + 1 < line.Length) && line[i + 1] == escape)
                                {
                                    escapedPrior = true;
                                }
                                else
                                {
                                    escaped = false;
                                }
                            }
                            else
                            {
                                escapedPrior = false;
                                sb.Append(c);
                            }
                        }
                        break;

                    case split:
                        if (escaped)
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            row.Columns.Add(new CsvColumn(sb.ToString()));
                            sb.Length = 0;
                        }
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            if (sb.Length > 0)
                row.Columns.Add(new CsvColumn(sb.ToString()));

            return row;
        }

        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return csvData;
        }
    }
}