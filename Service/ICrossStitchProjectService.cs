using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface ICrossStitchProjectService
    {
        Task<CrossStitchProject> CreateCrossStitchProjectAsync(CrossStitchProject item, Stream fileStream, string extension);
        void Update(CrossStitchProject crossStitchProject, Stream? newFileStream = null, string? extension = null);
        void Delete(CrossStitchProject crossStitchProject);
        Task<List<CrossStitchProject>> GetAllCrossStitchProjectsAsync();
    }
}
