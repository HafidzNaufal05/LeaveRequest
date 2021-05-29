using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeaveRequest.Repositories.Data
{
    public class RequestRepository : GeneralRepository<Request, MyContext, int>
    {
        private MyContext myContext;
        private readonly SendingEmail sendEmail;
        private readonly EmployeeRepository employeeRepository;

        public IConfiguration Configuration { get; }

        public RequestRepository(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration Configuration) : base(myContext)
        {
            this.myContext = myContext;
            this.sendEmail = new SendingEmail(myContext);
            this.employeeRepository = employeeRepository;
            this.Configuration = Configuration;
        }

        class Global
        {
            public static int resRequest;

        }
        public int Request(RequestVM requestVM)
        {
            var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            if (requestVM.ReasonRequest == "Melahirkan")
            {
                DateTime giveBirth = requestVM.StartDate.AddDays(92);

                var request = new Request()
                {
                    Employee = employee,
                    StartDate = requestVM.StartDate,
                    EndDate = giveBirth,
                    ReasonRequest = requestVM.ReasonRequest,
                    Notes = requestVM.Notes,
                    StatusRequest = StatusRequest.Waiting
                };

                myContext.Add(request);
                Global.resRequest = myContext.SaveChanges();

                sendEmail.SendRequestEmployee(employee);
                sendEmail.SendRequestManager(manager);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int ApprovalManager(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("HRD")).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.ApprovedByManager)
            {
                return 0;
            }

            if (data.StatusRequest == StatusRequest.Waiting)
            {
                data.StatusRequest = StatusRequest.ApprovedByManager;
                myContext.Update(data);

                sendEmail.SendRequestHRD(hrd);
                sendEmail.SendApproveManager(employee);

            }
            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }

        public int ApprovalHRD(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("HRD")).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.ApprovedByHRD)
            {
                return 0;
            }

            if (data.StatusRequest == StatusRequest.ApprovedByManager)
            {
                data.StatusRequest = StatusRequest.ApprovedByHRD;
                myContext.Update(data);

                sendEmail.SendApproveHRD(employee);
                sendEmail.SendApproveHRD(manager);

            }
            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }

        public int RejectManager(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("HRD")).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.RejectByManager)
            {
                return 0;
            }

            if (data.StatusRequest == StatusRequest.Waiting)
            {
                data.StatusRequest = StatusRequest.RejectByManager;
                myContext.Update(data);

                sendEmail.SendReject(employee);
            }
            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }

        public int RejectHRD(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("HRD")).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.RejectByHRD)
            {
                return 0;
            }

            if (data.StatusRequest == StatusRequest.ApprovedByManager)
            {
                data.StatusRequest = StatusRequest.RejectByHRD;
                myContext.Update(data);

                sendEmail.SendReject(employee);
                sendEmail.SendReject(manager);
            }
            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }

    }
}

