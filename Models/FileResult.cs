namespace GenerationService.Models
{
    public class FileResult
    {
        public bool IsSuccess { get; set; }
        public string? FilePath { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
