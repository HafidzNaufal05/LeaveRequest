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

        class Global
        {
            public static int resRequest;

        }
        public int Request(RequestVM requestVM)
        {
            var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();
            //var hrd = myContext.Employees.Where(e => e.NIK.Contains("HRD")).FirstOrDefault();
            var TotalDay = (requestVM.EndDate - requestVM.StartDate).TotalDays + 1;
/*            var sumMonth = (requestVM.EndDate - requestVM.StartDate).TotalDays;
            var TotalDaycek = requestVM.StartDate.AddMonths(0).AddDays(sumMonth);*/

            if (requestVM.LeaveCategory == 0) //SpecialRequest
            {
                if (requestVM.ReasonRequest == "Melahirkan")
                {
                    if (employee.Gender != "Female" && employee.MaritialStatus != "Married")
                    {
                        return 0;
                    }
                    else if (TotalDay != 92)
                    {
                        return 0;
                    }
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
                    var sendEmail = new EmailRequest(myContext);
                    sendEmail.SendRequestEmployee(employee, request.Id);
                    sendEmail.SendRequestManager(manager, employee, request.Id);
                    return 1;
                }

                else if (requestVM.ReasonRequest == "Kecelakaan")
                {
                    var request = new Request()
                    {
                        Employee = employee,
                        StartDate = requestVM.StartDate,
                        EndDate = requestVM.EndDate,
                        ReasonRequest = requestVM.ReasonRequest,
                        Notes = requestVM.Notes,
                        StatusRequest = StatusRequest.Waiting
                    };

                    myContext.Add(request);
                    Global.resRequest = myContext.SaveChanges();
                    var sendEmail = new EmailRequest(myContext);
                    sendEmail.SendRequestEmployee(employee, request.Id);
                    sendEmail.SendRequestManager(manager, employee, request.Id);
                    return 1;
                }
                
                else
                {

                    return 0;
                }
            }

            else //Normal Request
            {
                if (requestVM.ReasonRequest == "Married")
                {
                    if (TotalDay != 3)
                    {
                        return 0;
                    } 
                    else if (employee.MaritialStatus != "Single")
                    {
                        return 0;
                    }
                }
                else if (requestVM.ReasonRequest == "Marry or Circumcise or Baptize Children" ||
                    requestVM.ReasonRequest == "Wife gave birth or had a miscarriage" ||
                    requestVM.ReasonRequest == "Husband or Wife Parents or In laws Children or Son In law have passed away")
                {
                    if (TotalDay != 2)
                    {
                        return 0;
                    }

                }
                else if (requestVM.ReasonRequest == "Family member in one house died")
                {
                    if (TotalDay != 1)
                    {
                        return 0;
                    }
                }

                var request = new Request()
                {
                    Employee = employee,
                    StartDate = requestVM.StartDate,
                    EndDate = requestVM.EndDate,
                    ReasonRequest = requestVM.ReasonRequest,
                    Notes = requestVM.Notes,
                    StatusRequest = StatusRequest.Waiting,
                    LeaveCategory = requestVM.LeaveCategory
                };

                myContext.Add(request);
                Global.resRequest = myContext.SaveChanges();
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendRequestEmployee(employee, request.Id);
                sendEmail.SendRequestManager(manager, employee, request.Id);
                return 1;
            }
        }

        public int ApprovedManager(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            //var emp_role = myContext.Roles.Where(e => e.RoleName == e.Employee.NIK).FirstOrDefault();
            //var hrd = myContext.Employees.Include(Role).Where(e => e.NIK == emp_role).FirstOrDefault();
            //var hrd = myContext.Employees.Include(EmployeeRole).Where(e => e.Employee.NIK == e.EmployeeRole."4101").FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("4401HRD")).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();

            if (data == null)
            {
                return 0;
            }

            else if (data.StatusRequest == StatusRequest.Waiting)
            {
                data.StatusRequest = StatusRequest.ApprovedByManager;
                myContext.Update(data);

                sendEmail.SendRequestHRD(hrd,employee , data.Id, approveVM.Notes);

            }

            else if (data.StatusRequest == StatusRequest.ApprovedByManager || data.StatusRequest == StatusRequest.ApprovedByHRD || data.StatusRequest == StatusRequest.RejectByHRD || data.StatusRequest == StatusRequest.RejectByManager)
            {
                return 0;
            }

            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }

        public int ApprovedHRD(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("4401HRD")).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.ApprovedByHRD || data.StatusRequest == StatusRequest.RejectByHRD || data.StatusRequest == StatusRequest.RejectByManager || data.StatusRequest == StatusRequest.Waiting)
            {
                return 0;
            }
            else if (data.StatusRequest == StatusRequest.ApprovedByManager)
            {

                data.StatusRequest = StatusRequest.ApprovedByHRD;
                var TotalDay = (data.EndDate - data.StartDate).TotalDays;
                if (data.LeaveCategory != 0)
                {
                    employee.RemainingQuota = (int)(employee.RemainingQuota - TotalDay);
                }
                myContext.Update(data);

                sendEmail.SendApproveHRD(manager, employee, data.Id, approveVM.Notes);
            }
            else
            {
                return 0;
            }
            myContext.SaveChanges();
            return 1;
        }



        public int Reject(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();


            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            if (data.StatusRequest == StatusRequest.RejectByHRD || data.StatusRequest == StatusRequest.RejectByManager)
            {
                return 0;
            }

            if (data.StatusRequest == StatusRequest.Waiting)
            {
                data.StatusRequest = StatusRequest.RejectByManager;
                myContext.Update(data);

                sendEmail.SendReject(employee, data.Id, approveVM.Notes);
            }
            else if (data.StatusRequest == StatusRequest.ApprovedByManager)
            {
                data.StatusRequest = StatusRequest.RejectByHRD;
                myContext.Update(data);

                sendEmail.SendReject(employee, data.Id, approveVM.Notes);
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

