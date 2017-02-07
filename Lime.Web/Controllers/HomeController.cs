using Lime.BusinessLayout.EmailHelpers;
using Lime.BusinessLayout.ExcelReportsHelpers;
using Lime.Domain;
using Lime.Web.Models;
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
        public async Task<JsonResult> CreateReport(ReportFilterModel model)
        {
            string error = null;
            Guid fileId = Guid.Empty;
            try
            {
                SalesReport reportHelper = new SalesReport(DB);
                //Create excel file and get id file
                fileId = await reportHelper.CreateReportAsync(model.From, model.To);
                EmailHelper helper = new EmailHelper();
                //send file on emal
                await helper.SendSalesReport(fileId, model.From, model.To, model.Email);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                error,
                fileId
            });
        }
    }
}