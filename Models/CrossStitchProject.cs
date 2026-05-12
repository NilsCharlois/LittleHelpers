namespace LittleHelpers.Models
{
    public class CrossStitchProject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ImageFilename { get; set; } = string.Empty;
        public bool HasFabric { get; set; } = false;
        public bool IsKit { get; set; } = false;
    }
}
