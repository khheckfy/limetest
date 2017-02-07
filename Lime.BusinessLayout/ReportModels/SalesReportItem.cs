using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lime.BusinessLayout.ReportModels
{
    public class SalesReportItem
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int OrderId { set; get; }
        /// <summary>
        /// Артикул товара
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// Количество
        /// </summary>
        public short? Quantity { set; get; }
        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime? OrderDate { set; get; }
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { set; get; }
        /// <summary>
        /// Цена единицы товара
        /// </summary>
        public decimal? Price { set; get; }
    }
}
