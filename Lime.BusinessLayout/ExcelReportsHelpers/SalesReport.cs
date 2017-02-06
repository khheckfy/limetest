using Lime.Domain;
using OfficeOpenXml;
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

        public async Task<Guid> CreateReportAsync(DateTime? from, DateTime? to)
        {
            Guid fileId = Guid.NewGuid();
            string fileName = string.Format("{0}.xlsx", fileId);
            string fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", fileName);

            var list = await DB.OrderRepository.GetAllAsync();

            FileInfo newFile = new FileInfo(fullFileName);
            ExcelPackage pck = new ExcelPackage(newFile);

            var ws = pck.Workbook.Worksheets.Add(string.Format("Отчет_{0:dd.mm.yy}", DateTime.Now));
            int rowIndex = 1;

            #region report title create

            ws.Cells[rowIndex, 1].Value = "Отчет по продажам";
            ws.Cells[rowIndex, 1].Style.Font.Bold = true;
            ws.Cells[rowIndex, 1, rowIndex, 7].Merge = true;
            rowIndex++;

            ws.Cells[rowIndex, 1].Value = string.Format("Период: {0}-{1}", from, to);
            ws.Cells[rowIndex, 1].Style.Font.Bold = true;
            ws.Cells[rowIndex, 1, rowIndex, 7].Merge = true;
            rowIndex++;

            #endregion

            #region header create

            int col = 1;
            ws.Cells[rowIndex, col++].Value = "Номер заказ";
            ws.Cells[rowIndex, col++].Value = "Дата";
            ws.Cells[rowIndex, col++].Value = "Артикул";
            ws.Cells[rowIndex, col++].Value = "Наименование";
            ws.Cells[rowIndex, col++].Value = "Количество";
            ws.Cells[rowIndex, col++].Value = "Цена";
            ws.Cells[rowIndex, col++].Value = "Стоимость";
            rowIndex++;

            #endregion

            #region body report

            foreach (var item in list)
            {
                col = 1;

                rowIndex++;
            }

            #endregion

            pck.Save();

            return fileId;
        }
    }
}
