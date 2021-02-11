using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_Store.Controllers
{
    public class MailController : Controller
    {
        public string SendMail(string name, string mail, string subject, string message)
        {
            try
            {
                //Send mail
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("riddaali32@gmail.com");
                mailMessage.To.Add("pythoning7@gmail.com");
                mailMessage.To.Add("riddaali32@gmail.com"); 
                mailMessage.Subject = subject;
                mailMessage.Body = "Name: " + name + "\nMail: " + mail + "\nMessage: " + message;
                
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("riddaali32@gmail.com", "3dsdsoft195");
                smtpClient.Send(mailMessage);
                return "send";
            }
            catch (Exception ex)
            {
                return "fail";
            }

        }
    }
}