using Lime.BusinessLayout.ReportModels;
using Lime.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lime.BusinessLayout.ExcelReportsHelpers
{
    public class SalesReport
    {
        private static string currencyFormat = "$###,###,##0.00";
        private readonly IUnitOfWork DB;
        public SalesReport(IUnitOfWork db)
        {
            DB = db;
        }

        /// <summary>
        /// Получить данные для текущего отчета
        /// </summary>
        /// <param name="fromDate">дата с</param>
        /// <param name="toDate">дата по</param>
        /// <returns></returns>
        private async Task<List<SalesReportItem>> GetDataForReport(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1).Date;
            fromDate = fromDate.Date;

            var query = from order in DB.OrderRepository.Query()
                        join orderDetail in DB.OrderDetailRepository.Query() on order.ID equals orderDetail.OrderID
                        join product in DB.ProductRepository.Query() on orderDetail.ProductID equals product.ID
                        where
                            order.OrderDate >= fromDate
                            && order.OrderDate < toDate
                        orderby
                            order.OrderDate
                        select new SalesReportItem
                        {
                            OrderDate = order.OrderDate,
                            OrderId = order.ID,
                            Price = product.UnitPrice,
                            ProductId = product.ID,
                            ProductName = product.Name,
                            Quantity = orderDetail.Quantity
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// Формирование отчета
        /// </summary>
        /// <param name="fromDate">Дата с</param>
        /// <param name="toDate">Дата по</param>
        /// <returns>ID файла</returns>
        public async Task<Guid> CreateReportAsync(DateTime fromDate, DateTime toDate)
        {
            Guid fileId = Guid.NewGuid();
            string fileName = string.Format("{0}.xlsx", fileId);
            string fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", fileName);

            var list = await GetDataForReport(fromDate, toDate);

            FileInfo newFile = new FileInfo(fullFileName);
            using (ExcelPackage pck = new ExcelPackage(newFile))
            {

                var ws = pck.Workbook.Worksheets.Add(string.Format("Отчет_{0:dd.MM.yy}", DateTime.Now));
                int rowIndex = 1;

                #region report title create

                ws.Cells[rowIndex, 1].Value = "Отчет по продажам";
                ws.Cells[rowIndex, 1].Style.Font.Bold = true;
                ws.Cells[rowIndex, 1].Style.Font.Size = 18;
                ws.Cells[rowIndex, 1, rowIndex, 7].Merge = true;
                ws.Cells[rowIndex, 1, rowIndex, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowIndex++;

                ws.Cells[rowIndex, 1].Value = string.Format("Период: {0:dd.MM.yyyy}-{1:dd.MM.yyyy}", fromDate, toDate);
                ws.Cells[rowIndex, 1].Style.Font.Bold = true;
                ws.Cells[rowIndex, 1, rowIndex, 7].Merge = true;
                ws.Cells[rowIndex, 1, rowIndex, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowIndex++;

                #endregion

                #region header create

                rowIndex++;
                int startRowTableIndex = rowIndex;
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

                    ws.Cells[rowIndex, col++].Value = item.OrderId;

                    ws.Cells[rowIndex, col].Style.Numberformat.Format = "dd.mm.yyyy";
                    ws.Cells[rowIndex, col++].Value = item.OrderDate;


                    ws.Cells[rowIndex, col++].Value = item.ProductId;
                    ws.Cells[rowIndex, col++].Value = item.ProductName;
                    ws.Cells[rowIndex, col++].Value = item.Quantity;

                    ws.Cells[rowIndex, col].Style.Numberformat.Format = currencyFormat;
                    ws.Cells[rowIndex, col++].Value = item.Price;

                    
                    ws.Cells[rowIndex, col].Formula = string.Format("{0}*{1}", ws.Cells[rowIndex, col - 1].Address, ws.Cells[rowIndex, col - 2].Address);
                    ws.Cells[rowIndex, col].Style.Numberformat.Format = currencyFormat;

                    rowIndex++;
                }

                //add borders to data table
                var modelTable = ws.Cells[startRowTableIndex, 1, rowIndex, col];

                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelTable.AutoFitColumns();

                #endregion

                #region footer report

                col = 1;
                ws.Cells[rowIndex, col++].Value = "Итого";
                ws.Cells[rowIndex, col++].Value = string.Empty;
                ws.Cells[rowIndex, col++].Value = string.Empty;
                ws.Cells[rowIndex, col++].Value = string.Empty;
                ws.Cells[rowIndex, col++].Formula = string.Format("SUM({0})", ws.Cells[startRowTableIndex + 1, col - 1, rowIndex - 1, col - 1].Address);
                ws.Cells[rowIndex, col++].Value = string.Empty;

                ws.Cells[rowIndex, col].Style.Numberformat.Format = currencyFormat;
                ws.Cells[rowIndex, col++].Formula = string.Format("SUM({0})", ws.Cells[startRowTableIndex + 1, col - 1, rowIndex - 1, col - 1].Address);

                ws.Cells[rowIndex, 1, rowIndex, col].Style.Font.Bold = true;
                ws.Cells[rowIndex, 1, rowIndex, 4].Merge = true;

                #endregion

                pck.Save();
            }

            return fileId;
        }
    }
}
