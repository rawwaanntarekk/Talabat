namespace Talabat.Dashboard.Helpers
{
	public class PictureSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// Get Folder Path

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

			// File name should be unique

			var fileName = Guid.NewGuid() + file.FileName;

			// Get File Path

			var filePath = Path.Combine(folderPath, fileName);

			// Save file as stream

			var filestream = new FileStream(filePath, FileMode.Create);

			// Copy file into streams

			file.CopyTo(filestream);

			// return file name

			return Path.Combine("images\\products", fileName);

		}

		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName, fileName );

			if(File.Exists(filePath)) 
				File.Delete(filePath);
		}
	}
}
