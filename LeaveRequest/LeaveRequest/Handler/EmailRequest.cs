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

        public void SendRequestEmployee(Employee employee, int id)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Leave Request", "\nThank You For You Request. Your Request Is Being Processed With ID : " +  id);
        }

        public void SendRequestManager(Employee manager, Employee employee, int id)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, manager.Email, "Approval Leave Request", "\nEmployee with Name " + employee.FirstName + " " + employee.LastName + " And Id Request Is " + id + " Has Been Request. Please Approve Or Reject Employee Request ");
        }

       /* public void SendApproveManager(Employee employee, int id)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Leave Request", "\n Manager has been approve with Employee Name is " + employee.FirstName + " " + employee.LastName + " And Id Request Is " + id + "Please approve or reject employee request");
        }*/

        public void SendRequestHRD(Employee hrd, Employee employee, int id, string notes)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, hrd.Email, "Approval Leave Request", "\nManager has been approve with Employee Name is " + employee.FirstName + " " + employee.LastName + " And Id Request Is " + id + " Please approve or reject employee request" + "\nNotes : " + notes);
        }

        public void SendApproveHRD(Employee employee, int id, string notes)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Request ", "\nHello " + employee.FirstName + "  Your request with Id " + id + " has been approve" + "\nNotes : " + notes);
        }



        public void SendReject(Employee employee, int id, string notes)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Request ", "\nSorry your request with ID " + id + "  has been rejected" + "\nNotes : " + notes);
        }
    }
}
