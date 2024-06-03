using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogCentral.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        public async Task<string?> UploadAsync(IFormFile file)
        {
            Cloudinary cloudinary = new Cloudinary("cloudinary://661326635448137:kAol62PFggOu0VDSvkBvIiQ2huo@dfvbbjzae");
            cloudinary.Api.Secure = true;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true, 
                DisplayName = file.FileName,
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
