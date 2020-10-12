using System.Net.Mail;
using TeduShop.Common.Constants;

namespace TeduShop.Common.Utilities
{
    public class MailHelperUtility
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigHelperUtility.GetByKey(Constant.AppSetting_SMTPHost);
                var port = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_SMTPPort));
                var fromEmail = ConfigHelperUtility.GetByKey(Constant.AppSetting_FromEmailAddress);
                var password = ConfigHelperUtility.GetByKey(Constant.AppSetting_FromEmailPassword);
                var fromName = ConfigHelperUtility.GetByKey(Constant.AppSetting_FromName);

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch (SmtpException smex)
            {
                return false;
            }
        }
    }
}