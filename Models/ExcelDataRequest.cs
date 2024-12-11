namespace GenerationService.Models
{
    public class ExcelDataRequest
    {
        public string[] Columns { get; set; }
        public object[][] Values { get; set; }
    }
}
