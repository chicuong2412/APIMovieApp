using API.ENUMS.ErrorCodes;
using API.Exceptions;

namespace API.Services
{
    public class FileServices
    {
        public async Task<string> SaveFileAvatarAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new AppException(ErrorCodes.DataInvalid);
            }
            var uploadsFolder = Path.Combine("D:\\Second", "uploads");
            Directory.CreateDirectory(uploadsFolder); // Create folder if it doesn't exist

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
