using System.IO;
using Abp.Auditing;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using tibs.stem.Dto;
using System.Linq;
using Abp.IO.Extensions;
using tibs.stem.Web.Helpers;
using System.Drawing.Imaging;
using Abp.Extensions;
using System;
using Abp.Runtime.Session;
using tibs.stem.IO;
using System.Drawing;
using Abp.Web.Models;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.Products;
using Abp.Domain.Repositories;

namespace tibs.stem.Web.Controllers
{
    public class FileController : stemControllerBase
    {
        private readonly IAppFolders _appFolders;
        private readonly IRepository<Product> _productRepository;

        public FileController(IAppFolders appFolders, IRepository<Product> productRepository)
        {
            _appFolders = appFolders;
            _productRepository = productRepository;
        }

        [DisableAuditing]
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }

        public JsonResult UploadProductPicture(int ProductId)
        {
            try
            {
                var productPictureFile = Request.Form.Files.First();

                //Check input
                if (productPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductPicture_Change_Error"));
                }

                if (productPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = productPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }
                var Products = _appFolders.ProductFilePath + + ProductId;

                if (!Directory.Exists(Products))
                {
                    Directory.CreateDirectory(Products);
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.ProductFilePath, "productImage_" + ProductId);

                //Save new picture
                var fileInfo = new FileInfo(productPictureFile.FileName);
                var tempFileName = "productImage_" + ProductId + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.ProductFilePath, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
              

                }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadProductSpecificationPicture(int SpecificationId, string ImgPath)
        {
            try
            {
                var productSpecificationPictureFile = Request.Form.Files.First();

                //Check input
                if (productSpecificationPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductSpecificationPicture_Change_Error"));
                }

                if (productSpecificationPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductSpecificationPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = productSpecificationPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                //Delete old temp pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productSpecificationPictureFile.FileName);
                var tempFileName = productSpecificationPictureFile.FileName;
                var tempFilePath = Path.Combine(_appFolders.ProductSpecificationFilePath, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/ProductSpecification/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }

            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

    }
}