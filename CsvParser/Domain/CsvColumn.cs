namespace CsvParser.Domain
{
    public class CsvColumn
    {
        public string Value { get; }

        public CsvColumn(string value)
        {
            Value = value;
        }
    }
}
