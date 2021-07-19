using System.Linq;
using CsvParser.Domain;

namespace CsvParser.Models
{
    public class FormattedDisplayModel
    {
        public CsvTable CsvTable { get; set; }
        public string OriginalFileName { get; set; }

        public bool HasHeader()
        {
            return CsvTable?.HeaderRow?.Columns != null && CsvTable.HeaderRow.Columns.Any();
        }

        public bool HasBody()
        {
            return CsvTable?.Rows != null && CsvTable.Rows.Any();
        }
    }
}