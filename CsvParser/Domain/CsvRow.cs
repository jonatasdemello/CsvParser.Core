using System.Collections.Generic;

namespace CsvParser.Domain
{
    public class CsvRow
    {
        public List<CsvColumn> Columns { get; set; }

        public CsvRow()
        {
            Columns = new List<CsvColumn>();
        }

        public override string ToString()
        {
            return string.Join(",", Columns);
        }
    }
}

