﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeaveRequest.Handler
{
    public class sendEmail
    {
        public void SendForgotPassword2(string resetCode, string email)
        {
            var time24 = DateTime.Now.ToString("HH:mm:ss");

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("aninsabrina17@gmail.com", "yulisulasta");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            NetworkCredential nc = new NetworkCredential("aninsabrina17@gmail.com", "yulisulasta");
            smtp.Credentials = nc;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("aninsabrina17@gmail.com", "Leave Request Reset Password");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Reset Password " + time24;
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = "Hi " + "\nThis is new password for your account. " + resetCode + "\nThank You";
            smtp.Send(mailMessage);
        }
    }
}
