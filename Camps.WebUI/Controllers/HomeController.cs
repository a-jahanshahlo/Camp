using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Camps.DataLayer.Context;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ICampService _campService;
        private IUnitOfWork _unitOfWork;
        public HomeController(ICampService campService,IUnitOfWork unitOfWork)
        {
            
            _campService = campService;
         _unitOfWork =   unitOfWork;
        }
        public ActionResult Index()
        {
            ViewBag.Message ="Camp Count = "+ _campService.GetAll().Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetEvent()
        {
            var result = new
            {
                Name = "Alireza",
                Family = "Jahanshahlo",
                Web = "http",
                Tel = "12345",
                location = new
                {
                    Address = "tehran-wolfare",
                    City = "tehran",
                    No = "251"
                },
                imageUrl = "/images/orderedList8.png",
                sessions = new[]
                {
                    new
                    {
                        Phone = "First Phone:1234",
                        Vote = 2
                    },
                    new
                    {
                        Phone = "First Phone:56789",
                        Vote = 12
                    },
                    new
                    {
                        Phone = "First Phone:101112",
                        Vote = 32
                    }
                }

            };
            
      
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
