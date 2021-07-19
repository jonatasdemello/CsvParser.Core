using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvParser.Services;

namespace CsvParser.Test.Unit
{
    /// <summary>
    /// This class should have content.
    /// Feel free to use any testing framework you desire. (i.e. NUnit, XUnit, Microsoft built-in testing framework)
    /// You may also use a mocking framework (i.e. Moq, RhinoMock)
    ///
    /// If you've never done unit testing before, don't worry about this section and look to complete some of the bonus mark tasks
    /// </summary>
    [TestClass]
    public class ParsingServiceTest
    {
        [TestMethod, TestCategory("Parsing")]
        public void parsing_empty_should_return_false()
        {
            var p = new ParsingService();
            var s = p.ParseCsv("", false);
            Assert.IsFalse(s == null);
        }

        [TestMethod, TestCategory("Parsing")]
        public void parsing_should_return_true()
        {
            var p = new ParsingService();
            var s = p.ParseCsv("Id,Name\r\n1,Art\r\n2,Language\r\n3,Math\r\n4,Gym\r\n5,Science\r\n", true);
            Assert.IsFalse(s == null);
        }

        [TestMethod]
        public void ParseCsvHeaderedTest()
        {
            var rawCsv = @"Header1,Header2
                           Row1Value1,Row1Value2
                           Row2Value1,Row2Value2";

            var parsingService = new ParsingService();
            var csvTable = parsingService.ParseCsv_v1(rawCsv, true);

            Assert.IsNull(csvTable.HeaderRow);
            Assert.AreEqual(2, csvTable.HeaderRow.Columns.Count);
            Assert.AreEqual(2, csvTable.Rows.Count);
            Assert.AreEqual("Header1", csvTable.HeaderRow.Columns[0].Value);
            Assert.AreEqual("Header2", csvTable.HeaderRow.Columns[1].Value);
            Assert.AreEqual("Row1Value1", csvTable.Rows[0].Columns[0].Value);
            Assert.AreEqual("Row1Value2", csvTable.Rows[0].Columns[1].Value);
            Assert.AreEqual("Row2Value1", csvTable.Rows[1].Columns[0].Value);
            Assert.AreEqual("Row2Value2", csvTable.Rows[1].Columns[1].Value);
        }

        [TestMethod]
        public void ParseCsvUnheaderedTest()
        {
            var rawCsv = @"Row1Value1,Row1Value2
                           Row2Value1,Row2Value2";

            var parsingService = new ParsingService();
            var csvTable = parsingService.ParseCsv(rawCsv, false);

            Assert.IsNull(csvTable.HeaderRow);
            Assert.AreEqual(2, csvTable.Rows.Count);
            Assert.AreEqual("Row1Value1", csvTable.Rows[0].Columns[0].Value);
            Assert.AreEqual("Row1Value2", csvTable.Rows[0].Columns[1].Value);
            Assert.AreEqual("Row2Value1", csvTable.Rows[1].Columns[0].Value);
            Assert.AreEqual("Row2Value2", csvTable.Rows[1].Columns[1].Value);
        }

        [TestMethod]
        public void ParseRow()
        {
            var line = @"Value1,""Value, 2!"",Value 3";

            var parsingService = new ParsingService();
            var row = parsingService.ParseRow_v1(line);

            Assert.AreEqual("Value1", row.Columns[0].Value);
            Assert.AreEqual("Value, 2!", row.Columns[1].Value);
            Assert.AreEqual("Value 3", row.Columns[2].Value);
        }
    }
}