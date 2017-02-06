using Lime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lime.Web.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork DB;
        public HomeController(IUnitOfWork _db)
        {
            DB = _db;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}