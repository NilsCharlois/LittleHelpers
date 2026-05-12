using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class CrossStitchProjectService : ICrossStitchProjectService
    {
        private Models.AppContext context;
        private IWebHostEnvironment _env;

        public CrossStitchProjectService(Models.AppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            _env = env;
        }

        public Task<List<CrossStitchProject>> GetAllCrossStitchProjectsAsync()
        {
            return context.CrossStitchProjects.ToListAsync();
        }

        public async Task<CrossStitchProject> CreateCrossStitchProjectAsync(CrossStitchProject crossStitchProject, Stream fileStream, string extension)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = await SaveFile(fileStream, extension);

            crossStitchProject.ImageFilename = fileName;

            context.CrossStitchProjects.Add(crossStitchProject);
            await context.SaveChangesAsync();

            return crossStitchProject;
        }

        public async void Update(CrossStitchProject crossStitchProject, Stream? newFileStream = null, string? extension = null)
        {
            var existing = context.CrossStitchProjects.FirstOrDefault(x => x.Id == crossStitchProject.Id);

            if (existing == null)
                return;

            existing.Name = crossStitchProject.Name;
            existing.Description = crossStitchProject.Description;
            existing.IsKit = crossStitchProject.IsKit;
            existing.HasFabric = crossStitchProject.HasFabric;

            // New image uploaded
            if (newFileStream != null && extension != null)
            {
                var oldImagePath = existing.ImageFilename;

                // Save new image first
                var newRelativePath = await SaveFile(newFileStream, extension);

                // Update DB
                existing.ImageFilename = newRelativePath;

                context.SaveChanges();

                // Delete old image AFTER success
                DeleteImage(oldImagePath);
            }
            else
            {
                context.SaveChanges();
            }
        }

        private async Task<string> SaveFile(Stream stream, string extension)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(uploadsFolder, fileName);

            using var fs = new FileStream(fullPath, FileMode.Create);

            await stream.CopyToAsync(fs);

            return fileName;
        }

        public void Delete(CrossStitchProject crossStitchProject)
        {
            var existing = context.CrossStitchProjects.FirstOrDefault(x => x.Id == crossStitchProject.Id);
            if (existing != null)
            {
                DeleteImage(existing.ImageFilename);
                context.Entry(existing).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private void DeleteImage(string fileName)
        {
            var fullPath = Path.Combine(_env.WebRootPath, "Images", fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
