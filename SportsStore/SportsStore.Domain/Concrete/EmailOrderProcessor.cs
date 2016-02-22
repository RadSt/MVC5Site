using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Razor.Parser;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAdress = "orders@eample.com";
        public string MailFromAdress = "sportsstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUserName";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"d:\sports_store_emails";
    }
    public class EmailOrderProcessor:IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpclient = new SmtpClient())
            {
                smtpclient.EnableSsl = emailSettings.UseSsl;
                smtpclient.Host = emailSettings.ServerName;
                smtpclient.Port = emailSettings.ServerPort;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials=new NetworkCredential(emailSettings.Username,
                    emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpclient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpclient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpclient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price*line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", line.Quantity,
                        line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}",
                        shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage=new MailMessage(
                    emailSettings.MailFromAdress,//От
                    emailSettings.MailToAdress,//Кому
                    "New order submitted!",//Тема
                    body.ToString());//Тело
            }
        }
    }
}