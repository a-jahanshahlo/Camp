using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using AutoMapper;
using Camps.CommonLib.ExtendObjects;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Camps;
using Domain = Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Camps.WebUI.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        public FilesController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;

        }
        //[HttpPost, ActionName("Upload")] // This is from System.Web.Http, and not from System.Web.Mvc
        //public async Task<HttpResponseMessage> Upload()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var provider = GetMultipartProvider();
        //    var result = await Request.Content.ReadAsMultipartAsync(provider);
        //    //  Stream stream = result.GetStream(null, null);
        //    // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
        //    // so this is how you can get the original file name
        //    var originalFileName = GetFileInfo.GetDeserializedFileName(result.FileData.First());

        //    // uploadedFileInfo object will give you some additional stuff like file length,
        //    // creation time, directory name, a few filesystem methods etc..
        //    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

        //    // Remove this line as well as GetFormData method if you're not 
        //    // sending any form data with your upload request
        //    var fileUploadObj = GetFormData<UploadDataModel>(result);

        //    // Through the request response you can return an object to the Angular controller
        //    // You will be able to access this in the .success callback through its data attribute
        //    // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
        //    var returnData = "ReturnTest";
        //    return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        //}

        //// You could extract these two private methods to a separate utility class since
        //// they do not really belong to a controller class but that is up to you
        //private MultipartFormDataStreamProvider GetMultipartProvider()
        //{
        //    var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
        //    var root = HttpContext.Current.Server.MapPath(uploadFolder);
        //    Directory.CreateDirectory(root);
        //    return new MultipartFormDataStreamProvider(root);
        //}
        private MultipartFormDataMemoryStreamProvider GetMultipartMemoryStreamProvider()
        {
            return new MultipartFormDataMemoryStreamProvider();
        }
        //// Extracts Request FormatData as a strongly typed model
        //private object GetFormData<T>(MultipartFormDataStreamProvider result)
        //{
        //    if (result.FormData.HasKeys())
        //    {
        //        var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
        //        if (!String.IsNullOrEmpty(unescapedFormData))
        //            return JsonConvert.DeserializeObject<T>(unescapedFormData);
        //    }

        //    return null;
        //}


        //public class UploadDataModel
        //{
        //    public string testString1 { get; set; }
        //    public string testString2 { get; set; }
        //}

        //
        //[HttpPost]
        //  public async Task<HttpResponseMessage> Post()
        //  {
        //      var httpRequestHeaders = Request.Headers;
        //      Stream requestStream = await this.Request.Content.ReadAsStreamAsync();

        //      byte[] byteArray = null;
        //      using (MemoryStream ms = new MemoryStream())
        //      {
        //          await requestStream.CopyToAsync(ms);
        //          byteArray = ms.ToArray();
        //      }
        //      var photo = _photoService.Create();
        //      // photo.Image = byteArray;
        //      // photo.IsDeleted = false;
        //      // photo.UploadDate = DateTime.UtcNow;
        //      // photo.Guid = Guid.NewGuid();

        //      _photoService.Add(photo);
        //      _unitOfWork.SaveChanges();

        //      return Request.CreateResponse(HttpStatusCode.OK);
        //  }

        [HttpGet, ActionName("Download")]
        public async Task<HttpResponseMessage> Download(Guid id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var file = _fileService.Find(x => x.Guid == id);


            VideoStream videoStream = new VideoStream(file.FileBinary.ToStream());
            response.Content = new PushStreamContent(videoStream.WriteToStream, new MediaTypeHeaderValue(file.Extention));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            return await Task.FromResult(response);
        }

        [HttpPost, ActionName("UploadFile")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartMemoryStreamProvider();

            var tt = await Request.Content.ReadAsMultipartAsync(provider).ContinueWith(p =>
                {
                    var result = p.Result;
                    // var myParameter = result.FormData.GetValues("myParameter").FirstOrDefault();
                    var file = _fileService.Create();
                    foreach (var stream in result.Contents.Where((content, idx) => result.IsStream(idx)))
                    {
                        string fileName = GetFileInfo.GetDeserializedFileName(stream);
                        var contentType = GetFileInfo.GetContentType(stream);

                        if (_fileService.IsValidType(fileName) && _fileService.IsValidContent(contentType.MediaType))
                        {
                            file.FileBinary = stream.ReadAsByteArrayAsync().Result;
                            file.Size = stream.Headers.ContentLength;
                            file.IsDeleted = false;
                            file.Extention = contentType.MediaType;
                            file.Guid = Guid.NewGuid();
                            file.Name = fileName;
                            file.UploadDate = DateTime.UtcNow;
                            //  var file = new this.GetFormData<string >(stream.Headers.ContentDisposition.FileName);
                            //  var contentTest =  stream.ReadAsByteArrayAsync();
                            // ... and so on, as per your original code.
                            _fileService.Add(file);
                            _unitOfWork.SaveChanges();
                        }

                    }
                    return file;
                });





            /*var result = await Request.Content.ReadAsMultipartAsync(provider);
             * 

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
              
             * var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            
             * var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // Remove this line as well as GetFormData method if you're not 
            // sending any form data with your upload request
            
             * var fileUploadObj = GetFormData<UploadDataModel>(result);

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
           
             * var returnData = "ReturnTest";
            
    
             */
            return this.Request.CreateResponse(HttpStatusCode.OK, new { id = tt.Id });

        }


        [HttpGet, ActionName("thumbnail")]
        //api/files/thumbnail/4afc9769-2d8b-4403-9b4c-187d60928258?&width=200&heigth=200
        public HttpResponseMessage GetImageThumbnail(Guid id, [FromUri]int? width, [FromUri]int? heigth)
        {
            int reWidth = width ?? 60;
            int reheigth = heigth ?? 50;
            HttpResponseMessage response = new HttpResponseMessage();
            var file = _fileService.Find(x => x.Guid == id);

            if (file.Extention.IsImage())
            {
                Image img = ImageHandler.GetImage(file.FileBinary);

                Image resizeImage = img.ResizeImage(new Size(reWidth, reheigth));

                response.Content = new ByteArrayContent(resizeImage.ImageToArray());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }
            else if (file.Extention.IsVideo())
            {
                VideoStream videoStream = new VideoStream(file.FileBinary.ToStream());
                response.Content = new PushStreamContent(videoStream.WriteToStream, new MediaTypeHeaderValue(file.Extention));
                //(file.FileBinary.ToStream(), httpContent, string.Empty);
            }
            else if (file.Extention.IsAudio())
            {
                VideoStream videoStream = new VideoStream(file.FileBinary.ToStream());
                response.Content = new PushStreamContent(videoStream.WriteToStream, new MediaTypeHeaderValue(file.Extention));
            }



            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        [HttpGet, ActionName("GetVideo")]
        //api/files/thumbnail/4afc9769-2d8b-4403-9b4c-187d60928258?&width=200&heigth=200
        public HttpResponseMessage GetVideo(Guid id)
        {
            var photo = _fileService.Find(x => x.Guid == id);

            Image img = ImageHandler.GetImage(photo.FileBinary);

            HttpResponseMessage response = new HttpResponseMessage { Content = null };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
        [HttpGet, ActionName("GetFile")]
        public HttpResponseMessage GetFile(Guid id)
        {
            var photo = _fileService.Find(x => x.Guid == id);

            Image img = ImageHandler.GetImage(photo.FileBinary);

            HttpResponseMessage response = new HttpResponseMessage { Content = null };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
        [HttpPost, ActionName("UpdateFile")]
        public HttpResponseMessage UpdateFile(FileViewModel model)
        {

            Domain.Binary file = _fileService.Find(x => x.Guid == model.Guid);
            if (file != null)
            {
                file.Desctiption = model.Desctiption;
                //  _fileService.Update(file);
                _unitOfWork.SaveChanges();

                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }

            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotAcceptable };
            return response;
        }
        [HttpDelete]
        public HttpResponseMessage DeleteFile(Guid id)
        {

            Domain.Binary file = _fileService.Find(x => x.Guid == id);
            if (file != null)
            {
                _fileService.Delete(file);
                _unitOfWork.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            return response;

        }
        [HttpGet, ActionName("GetFilethumb")]
        public HttpResponseMessage GetFilethumb(Guid id)
        {
            const int reWidth = 200;
            const int reheigth = 150;
            var photo = _fileService.Find(x => x.Guid == id);

            Image img = ImageHandler.GetThumbnail(photo.FileBinary, photo.Extention);

            Image resizeImage = img.ResizeImage(new Size(reWidth, reheigth));

            HttpResponseMessage response = new HttpResponseMessage { Content = new ByteArrayContent(resizeImage.ImageToArray()) };


            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

    }
}
