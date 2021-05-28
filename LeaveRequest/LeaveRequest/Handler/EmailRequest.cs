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
    public class EmailRequest
    {
        private readonly MyContext myContext;

        public EmailRequest(MyContext context)
        {
            this.myContext = context;
        }

        public void SendRequestEmployee(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Leave Request", "\nThank You For You Request. Your Request Is Being Processed.");
        }

        public void SendRequestHRD(string url, string token, Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Leave Request From HRD ", "\n, Employee Has Been Request. Please Approve Or Reject Employee Request ");
        }

        public void SendApproveHRD(string url, string token, Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval From HRD ", "\n HRD has been approve, Please approve or reject employee request");
        }

        public void SendApproveManager(string url, string token, Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Request ", "\n Your request has been approve");
        }

        public void SendReject(string url, string token, Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Request ", "\n Sorry your request has been rejected");
        }
    }
}
