using CsvParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    public class ValidationServiceTest
    {
        [TestMethod, TestCategory("Validation")]
        public void IsCsvFile_must_return_true_for_csv()
        {
            var v = new ValidationService();
            var result = v.IsCsvFile("myFile.csv");
            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("Validation")]
        public void IsCsvFile_must_return_false_for_txt()
        {
            var v = new ValidationService();
            var result = v.IsCsvFile("myFile.txt");
            Assert.IsFalse(result);
        }

        [TestMethod, TestCategory("Validation")]
        public void IsCsvFile_must_return_false_for_file()
        {
            var v = new ValidationService();
            var result = v.IsCsvFile("myFile");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCsvFileTest()
        {
            const string validFileName = "valid_csv_filename.csv";
            const string invalidFileName = "invalid_csv_filename.bmp";

            var validationService = new ValidationService();

            Assert.IsTrue(validationService.IsCsvFile(validFileName));
            Assert.IsFalse(validationService.IsCsvFile(invalidFileName));
        }
    }
}