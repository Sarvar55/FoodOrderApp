namespace FoodOrderApp.Utility
{
    public class FileUtility
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUtility(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFileToDirectory(IFormFile file, string directory)
        {
            try
            {
                string generatedFileName = GenerateFileName(file);
                string fullPath = CreateFullPath(generatedFileName, directory);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return generatedFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public void DeleteFile(string fileName, string directory)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, directory);
            var filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GenerateFileName(IFormFile file)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
        }

        private string CreateFullPath(string fileName, string directory)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, directory);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return Path.Combine(folderPath, fileName);
        }
    }
}
