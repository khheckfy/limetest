using Lime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<JsonResult> CreateReport(DateTime? from, DateTime? to)
        {
            BusinessLayout.ExcelReportsHelpers.SalesReport reportHelper = new BusinessLayout.ExcelReportsHelpers.SalesReport(DB);
            Guid fileId = await reportHelper.CreateReportAsync();

            await Task.FromResult(0);
            return Json(fileId);
        }
    }
}