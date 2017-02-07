using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lime.BusinessLayout.EmailHelpers
{
    public class EmailHelper
    {
        private async Task SendAsync(MailMessage message)
        {
            var smtpClient = new SmtpClient();
            await smtpClient.SendMailAsync(message);
        }

        /// <summary>
        /// Отправить отчет с вложением
        /// </summary>
        /// <param name="fileId">ID файла</param>
        /// <returns></returns>
        public async Task SendSalesReport(Guid fileId, DateTime from, DateTime to, string email)
        {
            MailMessage message = new MailMessage();

            string fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", string.Format("{0}.xlsx", fileId));

            message.IsBodyHtml = true;
            message.Body =
            message.Subject =
                string.Format("Отчет по продажам ({0:dd.MM.yyyy}-{1:dd.MM.yyyy})", from, to);
            message.To.Add(new MailAddress(email));
            message.Attachments.Add(new Attachment(fullFileName));

            await SendAsync(message);
        }
    }
}
