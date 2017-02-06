using Lime.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lime.BusinessLayout.ExcelReportsHelpers
{
    public class SalesReport
    {
        private readonly IUnitOfWork DB;
        public SalesReport(IUnitOfWork db)
        {
            DB = db;
        }

        public async Task<Guid> CreateReportAsync()
        {
            Guid fileId = Guid.NewGuid();
            string fileName = string.Format("{0}.xlsx", fileId);
            string fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", fileName);
            
            var list = await DB.OrderRepository.GetAllAsync();

            File.WriteAllText(fullFileName,"test");


            return fileId;
        }
    }
}
