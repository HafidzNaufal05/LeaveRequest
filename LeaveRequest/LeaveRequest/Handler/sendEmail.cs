using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeaveRequest.Handler
{
    public class SendEmail
    {
        private readonly MyContext myContext;
        public SendEmail(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public void SendForgotPassword(string token, Employee employee)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("aninsabrina17@gmail.com", "yulisulasta");
            smtp.Send("aninsabrina17@gmail.com", employee.Email, "Reset Password", "https://localhost:44338/api/Account/ResetPassword \n token : " + token);
        }

    }
}
