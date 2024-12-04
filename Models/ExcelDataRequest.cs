namespace GenerationService.Models
{
    public class ExcelDataRequest
    {
        public List<string> Columns { get; set; } = new();
        public List<List<string>> Values { get; set; } = new();
    }
}
