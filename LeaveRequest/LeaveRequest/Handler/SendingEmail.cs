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
    public class SendingEmail
    {
        private readonly MyContext myContext;
        public SendingEmail(MyContext myContext)
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

        public void SendRequestEmployee(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Leave Request";
            message.Body = @"Thank You For You Request. Your Request Is Being Processed.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        public void SendRequestHRD(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Approval Leave Request";
            message.Body = @"Employee has been request and manager has been approved. Please Approve Or Reject Employee Request.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        public void SendApproveHRD(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Approval request from HRD";
            message.Body = @"Leave request has been approved, Thank you.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        public void SendRequestManager(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Approval Leave Request";
            message.Body = @"New request has been arrived. Please approve or reject employee request.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        public void SendApproveManager(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Leave request from Managert";
            message.Body = @"Your request has been approve by Manager, please wait approval from your HRD.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        public void SendReject(Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Approval Request";
            message.Body = @"Sorry your request has been rejected.";
            SmtpClient smtpmail = new SmtpClient();

            System.Net.NetworkCredential smtpusercredential = new System.Net.NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

    }
}
