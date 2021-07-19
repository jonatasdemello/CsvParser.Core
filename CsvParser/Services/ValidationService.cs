using System;
using System.IO;
using CsvParser.Contracts;

namespace CsvParser.Services
{
    /// <summary>
    /// This file must be unit tested
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Takes in a file name and determines whether it is a csv file or not.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool IsCsvFile(string filename)
        {
            //TODO fill in your logic here
            //throw new NotImplementedException();

            return filename != null ? String.Equals(Path.GetExtension(filename), ".csv", StringComparison.OrdinalIgnoreCase) : false;

            // or
            // return String.IsNullOrEmpty(filename) ? false : filename.ToLower().Contains(".csv");
        }
    }
}