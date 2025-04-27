namespace AdminDashboard.Helpers
{
    public static class PictureSettings
    {
        public static string Upload(IFormFile file, string folderName)
        {
            // 1. Prepare the folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", folderName);

            // 2. Make the file name unique
            var fileName = Guid.NewGuid() + file.FileName;

            // 3. Combine the folder path and file name
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Save the file as a stream

            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);


            return Path.Combine("images//products", fileName);

        }

        public static void Delete(string fileName, string folderName)
        {
            // 1. Get the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", folderName, fileName);

            // 2. Check if the file exists
            if (File.Exists(filePath))
            {
                // 3. Delete the file
                File.Delete(filePath);
            }
        }
    }
}
