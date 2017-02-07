using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lime.Web.Models
{
    public class ReportFilterModel
    {
        [Required]
        /// <summary>
        /// Дата с
        /// </summary>
        public DateTime From { set; get; }
        [Required]
        /// <summary>
        /// Дата по
        /// </summary>
        public DateTime To { set; get; }
        [Required]
        /// <summary>
        /// Почта, на которую отправить сформированный отчет
        /// </summary>
        public string Email { set; get; }
    }
}