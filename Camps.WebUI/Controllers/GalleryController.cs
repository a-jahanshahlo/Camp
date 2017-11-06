using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.AppSettings;
using Camps.WebUI.ViewModels.Camps;
using Camps.WebUI.ViewModels.Galleries;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers
{
    public class GalleryController : ApiController
    {
        private readonly IFileService _fileService;
        private readonly IGalleryService _galleryService;
        private readonly IUnitOfWork _unitOfWork;
        public GalleryController(IUnitOfWork unitOfWork, IGalleryService galleryService, IFileService fileService)
        {
            _fileService = fileService;
            _galleryService = galleryService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost ]
        public HttpResponseMessage NewGalleriy([FromBody]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);

            }
            var galleriy = _galleryService.Create();
            galleriy.Name = name;
            galleriy.IsDeleted = false;
            _galleryService.Add(galleriy);
            _unitOfWork.SaveChanges();


            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [HttpDelete ]
        public HttpResponseMessage DeleteGalleriy( int id)
        {

            var gallery = _galleryService.Find(id);
            gallery.Files.Clear();
            gallery.IsDeleted = true;
            _galleryService.Delete(gallery);
            _unitOfWork.SaveChanges();


            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [HttpGet]
        public IEnumerable<GalleryShortViewModel> GetAllGalleries()
        {
          
            var galleries = _galleryService.GetAll()
                .OrderByDescending(x => x.Id)
                .ToList();
 
            var model = Mapper.Map<IList<Gallery>, IList<GalleryShortViewModel>>(galleries);

            return model;
        }
        [HttpGet ]
        public HttpResponseMessage GetGalleries(int id)
        {
            int skip = id < 0 ? 0 : id;
            var galleries = _galleryService.GetAll()
                .Include(x => x.Files)
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(100)
                .ToList();
            //AppSetting s = _appSettingService.Get();

            // _appSettingService.Save(s);
            var galleryViewModels = Mapper.Map<IList<Gallery>, IList<GalleryViewModel>>(galleries);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, galleryViewModels);
            return response;
        }
        [HttpGet ]
        public HttpResponseMessage GetGalleryById(int id)
        {

            var gallery = _galleryService.GetAll()
                .Include(x => x.Files)
                .FirstOrDefault(x => x.Id == id);
            //AppSetting s = _appSettingService.Get();

            // _appSettingService.Save(s);
            var galleryViewModels = Mapper.Map<Gallery, GalleryViewModel>(gallery);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, galleryViewModels);
            return response;
        }
        [HttpGet ]
        public HttpResponseMessage GetFileList(int skip)
        {

            var photos = _fileService.GetAll()
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            var fileViewModel = Mapper.Map<IList<Binary>, IList<FileViewModel>>(photos);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, fileViewModel);
            return response;

        }
        [HttpPost ]
        public HttpResponseMessage RenameGallery( EditGalleryViewModel model)
        {
            HttpResponseMessage response;
            var gallery = _galleryService.Find(model.Id);
            if (gallery == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "the gallery not found");
                return response;
            }
            gallery.Name = model.GalleryName;
            _unitOfWork.SaveChanges();
            response = Request.CreateResponse(HttpStatusCode.OK, "Selected gallery renamed with successfully");
            return response;
        }
        [HttpPost ]
        public HttpResponseMessage AddToGallery( AddedFileToGalleryViewModel model)
        {
            HttpResponseMessage response;
            var gallery = _galleryService.Find(model.Id);
            if (gallery == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "the gallery not found");
                return response;
            }
            foreach (var item in model.Files)
            {
                try
                {
                    var file = _fileService.Find(x => x.Guid == Guid.Parse(item.FileId));
                    if (file == null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "the file not found");
                        return response;
                    }
                    gallery.Files.Add(file);

                    _unitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                 
                }

            }
            response = Request.CreateResponse(HttpStatusCode.OK, "Add file to gallery with success");
            return response;

        }

    }
}
