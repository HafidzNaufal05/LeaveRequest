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
    public class RequestRepository : GeneralRepository<Request, MyContext, string>
    {
        private MyContext myContext;
        private readonly EmailRequest sendEmail;
        private readonly EmployeeRepository employeeRepository;

        public IConfiguration Configuration { get; }

        public RequestRepository(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration Configuration) : base(myContext)
        {
            this.myContext = myContext;
            this.sendEmail = new EmailRequest(myContext);
            this.employeeRepository = employeeRepository;
            this.Configuration = Configuration;
        }

        public int Request(RequestVM requestVM)
        {
            if (requestVM.ReasonRequest == "Melahirkan")
            {
                DateTime giveBirth = requestVM.StartDate.AddDays(92);
                var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
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
                var resRequest = myContext.SaveChanges();

                sendEmail.SendRequestEmployee(employee);
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int Approved (ApproveVM approveVM)
        {

        }
    }
}
