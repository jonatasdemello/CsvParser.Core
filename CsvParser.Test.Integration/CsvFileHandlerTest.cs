using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvParser.Test.Integration
{
    /// <summary>
    /// Bonus Task:
    /// We've provided a few testing files. Integration test the csv file handler using these files.
    /// Feel free to use any testing framework you desire. (i.e. NUnit, XUnit, Microsoft built-in testing framework)
    /// </summary>
    [TestClass]
    public class CsvFileHandlerTest
    {
        private const string JunkFileName = "junk.txt";
        private const string HeaderBlankLinesFileName = "sample.with.header.blank.lines.csv";
        private const string HeaderFileName = "sample.with.header.csv";
        private const string HeaderMissingFieldsFileName = "sample.with.header.missing.fields.csv";
        private const string NoHeaderThreeRowsFileName = "sample.without.header.3.rows.csv";
        private const string NoHeaderFileName = "sample.without.header.csv";

        private string GetFilePath(string fileName)
        {
            return $@"..\..\TestFiles\{fileName}";
        }

        //TODO integration test the CsvFileHandler here
    }
}
