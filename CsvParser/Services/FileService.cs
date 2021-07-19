using System;
using System.IO;
using System.Web;
using CsvParser.Contracts;
using Microsoft.AspNetCore.Http;

namespace CsvParser.Services
{
    /// <summary>
    /// This file does not need to be unit testable.
    /// Bonus Task:
    /// - Make this file unit testable using the adapter pattern for your file system interactions
    /// - Unit test this file
    /// </summary>
    public class FileService : IFileService
    {
        private const string UploadFilePath = "~/App_Data/uploads/";

        /// <summary>
        /// This file takes the file from the HttpPostedFileBase and saves the file to the appData folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The file path in the appData folder the file was saved to</returns>
        public string StoreFile(/*HttpPostedFileBase*/ IFormFile file)
        {
            //TODO fill in your logic here
            //throw new NotImplementedException();

            if (file != null && file.Length > 0)
            {
                //var destPath = System.Web.HttpContext.Current.Server.MapPath(UploadFilePath);
                //var fileName = Path.GetFileName(file.FileName);
                //var serverPath = Path.Combine(destPath, fileName);
                //file.SaveAs(serverPath);

                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    //await file.CopyToAsync(stream);
                }

                return filePath;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// This function takes in the filePath of a csv file stored in the app data folder and return the string content
        /// of that file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>The contents of the file in a string</returns>
        public string ReadFile(string filePath)
        {
            //TODO fill in your logic here
            //throw new NotImplementedException();

            if (File.Exists(filePath))
                return File.ReadAllText(filePath);
            else
                return "";
        }
    }
}