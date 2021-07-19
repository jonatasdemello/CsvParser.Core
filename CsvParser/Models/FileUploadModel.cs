using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace CsvParser.Models
{
    public class FileUploadModel
    {
        [Display(Name = "Please select to upload a CSV file")]
        public /*HttpPostedFileBase*/ IFormFile File { get; set; }

        [Display(Name = "Does the file contain a header?")]
        public bool ContainsHeader { get; set; }
    }
}