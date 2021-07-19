using System;

namespace CsvParser.Models
{
    public class ErrorViewModel : ErrorModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
