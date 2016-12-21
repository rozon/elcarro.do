using System;
using System.Net;
using System.Net.Mail;

namespace ElCarro.Web.EmailSender
{
    public class EmailSender
    {
        private MailAddress _fromAddress;
        private const string _fromPassword = "%2{%nf[ijZU*wgGJ4PrGrn774";

        public MailAddress toAddress { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public MailAddress fromAddress { get; set; }
        public string fromPassword { get; set; }
        public Attachment attachFile { get; set; }

        public EmailSender()
        {
            _fromAddress = new MailAddress("elcarro.do@gmail.com", "El Carro - Resultado");
        }

        /// <summary>
        /// This method send the email from the "elcarro.do@gmail.com"
        /// to the Email that was set in the constructor of the object
        /// </summary>
        /// <param name="text"></param>
        /// <param name="subject"></param>
        /// <param name="toAddress"></param>
        /// <returns></returns>
        public bool SendEmailFromElCarro()
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(subject) || null == toAddress)
            {
                return false;
            }

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_fromAddress.Address, _fromPassword),
                    Timeout = 20000
                };

                using (var mailMessage = new MailMessage(_fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = text
                })
                {
                    if (null != attachFile)
                        mailMessage.Attachments.Add(attachFile);

                    smtp.Send(mailMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
