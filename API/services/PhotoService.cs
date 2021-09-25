using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> _conf)
        {
            var account=new Account(
                _conf.Value.Name,
                _conf.Value.Key,
                _conf.Value.Secret
            );

            _cloudinary=new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photo)
        {
            var result=new ImageUploadResult();
            if(photo.Length>0){
                using var stream=photo.OpenReadStream();

                var uploadParams=new ImageUploadParams
                {
                     File=new FileDescription(photo.Name,stream),
                     Transformation=new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                result=await _cloudinary.UploadAsync(uploadParams);
        }

        return result;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string photoPubId)
        {
            var deleteParams=new DeletionParams(photoPubId);

            var result=await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}