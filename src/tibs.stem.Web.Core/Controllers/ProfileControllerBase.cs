using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using tibs.stem.IO;
using tibs.stem.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Abp.Domain.Repositories;
using tibs.stem.Products;

namespace tibs.stem.Web.Controllers
{
    public abstract class ProfileControllerBase : stemControllerBase
    {

        private readonly IAppFolders _appFolders;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProfileControllerBase(IAppFolders appFolders, IHostingEnvironment hostingEnvironment)
        {
            _appFolders = appFolders;
            _hostingEnvironment = hostingEnvironment;
        }


        public JsonResult UploadProfilePicture()
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (profilePictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "userProfileImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(profilePictureFile.FileName);
                var tempFileName = "userProfileImage_" + AbpSession.GetUserId() + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
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
        public JsonResult UploadProductPicture(int ProductId,string ImgPath)
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

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productPictureFile.FileName);
                var tempFileName = productPictureFile.FileName;
                var tempFilePath = Path.Combine(_appFolders.ProductFilePath, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/Product/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }

            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
        public JsonResult UploadColorCodePicture(int ProductId, string ImgPath)
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

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productPictureFile.FileName);
                var tempFileName = productPictureFile.FileName;
                var tempFilePath = Path.Combine(_appFolders.ColorCodeFilePath, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/ColorCode/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
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
        public JsonResult UploadMultiProductPicture(int ProductId)
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
                var Products = _appFolders.ProductFilePath + @"\" + ProductId;

                if (!Directory.Exists(Products))
                {
                    Directory.CreateDirectory(Products);
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(Products, productPictureFile.FileName);

                //Save new picture
                var fileInfo = new FileInfo(productPictureFile.FileName);
                var tempFileName = productPictureFile.FileName;
                var tempFilePath = Path.Combine(Products, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/Product/" + ProductId +"/"+ tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadQuotationProduct()
        {
            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\Import\\";
            try
            {
                if (!Directory.Exists(QuotationPath))
                {
                    Directory.CreateDirectory(QuotationPath);
                }
                var productFile = Request.Form.Files.First();

                //Check input
                if (productFile == null)
                {
                    throw new UserFriendlyException(L("ProductSpecificationPicture_Change_Error"));
                }

                if (productFile.ContentType != "text/plain")
                {
                    throw new UserFriendlyException("Uploaded file is not an accepted text file !");
                }

                byte[] fileBytes;
                using (var stream = productFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

              
                AppFileHelper.DeleteFilesInFolderIfExists(QuotationPath, productFile.FileName);

                //Save new file
                var fileInfo = new FileInfo(productFile.FileName);
                var tempFileName = productFile.FileName;
                var tempFilePath = Path.Combine(QuotationPath, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                return Json(new AjaxResponse(new { fileName = QuotationPath + tempFileName, name = tempFileName }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
        public void DeleteMultiProductPicture(string ProductUrl)
        {
            var filepath = ProductUrl;
            var tempFilePath = Path.Combine(_appFolders.FindFilePath, filepath);
            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }
        }

        public JsonResult UploadMultiTempProductPicture(int TempProductId)
        {
            try
            {
                var TempProductPictureFile = Request.Form.Files.First();

                //Check input
                if (TempProductPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductPicture_Change_Error"));
                }

                if (TempProductPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = TempProductPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }
                var TempProducts = _appFolders.TempProductFilePath + @"\" + TempProductId;

                if (!Directory.Exists(TempProducts))
                {
                    Directory.CreateDirectory(TempProducts);
                }

                //Delete old temp product pictures
                AppFileHelper.DeleteFilesInFolderIfExists(TempProducts, TempProductPictureFile.FileName);

                //Save new picture
                var fileInfo = new FileInfo(TempProductPictureFile.FileName);
                var tempFileName = TempProductPictureFile.FileName;
                var tempFilePath = Path.Combine(TempProducts, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/TemporaryProduct/" + TempProductId + "/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public void DeleteMultiTempProductPicture(string TempProductUrl)
        {
            var filepath = TempProductUrl;
            var tempFilePath = Path.Combine(_appFolders.FindFilePath, filepath);
            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }
        }
    }
}