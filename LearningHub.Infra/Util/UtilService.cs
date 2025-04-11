using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LearningHub.Infra.Util
{
    public class UtilService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        // ✅ Save image and return the file name
        public async Task<string?> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null) return null;

            string uploadsPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
            Directory.CreateDirectory(uploadsPath);

            string uniqueImageName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            string imagePath = Path.Combine(uploadsPath, uniqueImageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueImageName;
        }

        // ✅ Generate full public URL for an image
        public string? GetImageUrl(string? imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return null;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            return $"{baseUrl}/images/{imageName}";
        }

        // ✅ Public method to get the full profile image URL
        public string? GetProfileImageUrl(string? imageName)
        {
            return GetImageUrl(imageName);
        }
    }
}