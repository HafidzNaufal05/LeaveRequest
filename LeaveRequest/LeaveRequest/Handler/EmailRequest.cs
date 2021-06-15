using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.ViewModels;
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

        //public void SendForgotPassword(string token, Employee employee)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

        //    smtp.UseDefaultCredentials = false;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtp.Send(SendEmail.Name, employee.Email, "Reset Password", "https://localhost:44338/api/Account/ResetPassword \n token : " + token);
        //}


        public void SendRequestEmployee(Employee employee, /*int id,*/ RequestVM requestVM)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Leave Request", "\nThank you for you request." +
                                                                        //"\nRequest ID : " + id +
                                                                        "\nNIK : " + requestVM.EmployeeNIK +
                                                                        "\nName : " + employee.FirstName +" "+ employee.LastName +
                                                                        "\nStart Date : " + requestVM.StartDate +
                                                                        "\nEnd Date : " + requestVM.EndDate +
                                                                        "\nReason : " + requestVM.ReasonRequest);
        }

        public void SendRequestManager(Employee manager, Employee employee, /*int id,*/ RequestVM requestVM)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, manager.Email, "Approval Leave Request", "\nEmployee has been requested." + 
                                                                                "\nNIK : " + requestVM.EmployeeNIK +
                                                                                "\nName : " + employee.FirstName + " " + employee.LastName +
                                                                                //"\nRequest ID : " + id +
                                                                                "\nStart Date : " + requestVM.StartDate +
                                                                                "\nEnd Date : " + requestVM.EndDate +
                                                                                "\nReason : " + requestVM.ReasonRequest +
                                                                                "\nPlease response this request.");
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

        public void SendRequestHRD(Employee hrd, Employee employee, int id, Request data, string notes)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, hrd.Email, "Approval Leave Request", "\nManager has been approve this request." +
                                                                                "\nNIK : " + data.Employee.NIK +
                                                                                "\nName : " + employee.FirstName + " " + employee.LastName +
                                                                                "\nRequest ID : " + id +
                                                                                "\nStart Date : " + data.StartDate +
                                                                                "\nEnd Date : " + data.EndDate +
                                                                                "\nReason : " + data.ReasonRequest +
                                                                                "\nNotes : " + notes +
                                                                                "\nPlease response this request.");
        }

        public void SendApproveHRD(Employee manager, Employee employee, Request data, string notes)
        {
            var SendEmail = myContext.SendEmails.Find(1);


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(SendEmail.Name, SendEmail.Value);

            smtp.Send(SendEmail.Name, employee.Email, "Approval Request ", "\nLeave Request Data : " +
                                                                                        "\nNIK : " + data.Employee.NIK +
                                                                                        "\nName : " + employee.FirstName + " " + employee.LastName +
                                                                                        "\nRequest ID : " + data.Id +
                                                                                        "\nStart Date : " + data.StartDate +
                                                                                        "\nEnd Date : " + data.EndDate +
                                                                                        "\nReason : " + data.ReasonRequest +
                                                                                        "\nManager NIK : " + manager.NIK +
                                                                                        "\nStatus : Has Been Approved" +
                                                                                        "\nNotes : " + notes);

            smtp.Send(SendEmail.Name, manager.Email, "Approval Request ", "\nLeave Request \"\nNIK : " + data.Employee.NIK +
                                                                                        "\nName : " + employee.FirstName + " " + employee.LastName +
                                                                                        "\nRequest ID : " + data.Id +
                                                                                        "\nStart Date : " + data.StartDate +
                                                                                        "\nEnd Date : " + data.EndDate +
                                                                                        "\nReason : " + data.ReasonRequest +
                                                                                        "\nManager NIK : " + manager.NIK +
                                                                                        "\nStatus : Has Been Approved" +
                                                                                        "\nNotes : " + notes);
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


        public void SendForgotPassword(string token, Employee employee)
        {
            var SendEmail = myContext.SendEmails.Find(1);

            MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
            MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Reset Password";
            message.Body = "Please akses this link for reset your password : https://localhost:44350/Auth/Index";
            SmtpClient smtpmail = new SmtpClient();

            NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.EnableSsl = true;
            smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpmail.Credentials = smtpusercredential;
            smtpmail.Host = "smtp.gmail.com";
            smtpmail.Port = 587;
            smtpmail.Send(message);
        }

        //public void SendRequestEmployee(Employee employee, int id)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
        //    MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Leave Request";
        //    message.Body = @"Thank You For You Request. Your Request Is Being Processed. With ID :" + id;
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);
        //}

        //public void SendRequestManager(Employee manager, Employee employee, int id)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name);
        //    MailAddress to = new MailAddress(manager.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Approval Leave Request";
        //    message.Body = @"Employee Has Been Request with"+"\n NIK : "+employee.NIK+"\nName : " + employee.FirstName + " " + employee.LastName + "\n ID Request : " + id + "\nHas Been Request. Please Approve Or Reject Employee Request";
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);

        //}

        //public void SendApproveManager(Employee employee, int id)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
        //    MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Leave request from Managert";
        //    message.Body = @"Manager has been approve with Employee Name is " + employee.FirstName + " " + employee.LastName + " And Id Request Is " + id + "Please approve or reject employee request";
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);
        //}

        //public void SendRequestHRD(Employee hrd, Employee employee, int id, string notes)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
        //    MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Approval Leave Request";
        //    message.Body = @"Manager has been approve with Employee Name is " + employee.FirstName + " " + employee.LastName + " And Id Request Is " + id + " Please approve or reject employee request" + "\nNotes : " + notes;
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);
        //}

        //public void SendApproveHRD(Employee employee, int id, string notes)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
        //    MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Approval request from HRD";
        //    message.Body = @"Hello " + employee.FirstName + "  Your request with Id " + id + " has been approve" + "\nNotes : " + notes;
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);
        //}

        //public void SendReject(Employee employee, int id, string notes)
        //{
        //    var SendEmail = myContext.SendEmails.Find(1);

        //    MailAddress from = new MailAddress(SendEmail.Name, "Leave Request");
        //    MailAddress to = new MailAddress(employee.Email, employee.FirstName + " " + employee.LastName);
        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = "Approval Request";
        //    message.Body = @"Sorry your request with ID " + id + "  has been rejected" + "\nNotes : " + notes;
        //    SmtpClient smtpmail = new SmtpClient();

        //    NetworkCredential smtpusercredential = new NetworkCredential(SendEmail.Name, SendEmail.Value);
        //    smtpmail.UseDefaultCredentials = false;
        //    smtpmail.EnableSsl = true;
        //    smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpmail.Credentials = smtpusercredential;
        //    smtpmail.Host = "smtp.gmail.com";
        //    smtpmail.Port = 587;
        //    smtpmail.Send(message);
        //}
    }
}
