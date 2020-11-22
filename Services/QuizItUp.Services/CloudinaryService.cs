namespace QuizItUp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class CloudinaryService
    {
        public static async Task<string> UploadPicture(Cloudinary cloudinary, IFormFile picture, string pictureName, string folderName)
        {
            byte[] destinationImage;

            await using (var ms = new MemoryStream())
            {
                await picture.CopyToAsync(ms);

                destinationImage = ms.ToArray();
            }

            await using var memoryStream = new MemoryStream(destinationImage);

            pictureName = pictureName.Replace("&", "");
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(pictureName, memoryStream),
                Folder = folderName,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult?.SecureUri.AbsoluteUri;
        }

        //public static async Task DeletePicture(Cloudinary cloudinary, string pictureName)
        //{
        //    var delParams = new DelDerivedResParams().;

        //    await cloudinary.DeleteDerivedResourcesAsync(delParams);
        //}
        //UploadImag by byte array in exact folder
        //Delete img
    }
}
