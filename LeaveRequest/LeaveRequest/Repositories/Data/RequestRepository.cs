using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Interfaces;
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
        private readonly IGenericDapper dapper;
        public IConfiguration Configuration { get; }

        public RequestRepository(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration Configuration, IGenericDapper dapper) : base(myContext)
        {
            this.myContext = myContext;
            this.sendEmail = new EmailRequest(myContext);
            this.employeeRepository = employeeRepository;
            this.Configuration = Configuration;
            this.dapper = dapper;
        }

        //class Global
        //{
        //    public static int resRequest;

        //}
        //public int Request(RequestVM requestVM)
        //{
        //    var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
        //    var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();
        //    var TotalDay = (requestVM.EndDate - requestVM.StartDate).TotalDays + 1;

        //    if (requestVM.LeaveCategory == "Special Request") //SpecialRequest
        //    {
        //        if (requestVM.ReasonRequest == "Maternity Leave")
        //        {
        //            if (employee.Gender != "Female" && employee.MaritialStatus != "Married")
        //            {
        //                return 0;
        //            }
        //            else if (TotalDay != 92)
        //            {
        //                return 0;
        //            }
        //            DateTime giveBirth = requestVM.StartDate.AddDays(92);

        //            var request = new Request()
        //            {
        //                Employee = employee,
        //                StartDate = requestVM.StartDate,
        //                EndDate = giveBirth,
        //                ReasonRequest = requestVM.ReasonRequest,
        //                Notes = requestVM.Notes,
        //                StatusRequest = "Waithing"
        //            };

        //            myContext.Add(request);
        //            Global.resRequest = myContext.SaveChanges();
        //            var sendEmail = new EmailRequest(myContext);
        //            sendEmail.SendRequestEmployee(employee, request.Id);
        //            sendEmail.SendRequestManager(manager, employee, request.Id);
        //            return 1;
        //        }

        //        else if (requestVM.ReasonRequest == "Accident Leave")
        //        {
        //            var request = new Request()
        //            {
        //                Employee = employee,
        //                StartDate = requestVM.StartDate,
        //                EndDate = requestVM.EndDate,
        //                ReasonRequest = requestVM.ReasonRequest,
        //                Notes = requestVM.Notes,
        //                StatusRequest = "Waithing"
        //            };

        //            myContext.Add(request);
        //            Global.resRequest = myContext.SaveChanges();
        //            var sendEmail = new EmailRequest(myContext);
        //            sendEmail.SendRequestEmployee(employee, request.Id);
        //            sendEmail.SendRequestManager(manager, employee, request.Id);
        //            return 1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }

        //    else //Normal Request
        //    {
        //        if (requestVM.ReasonRequest == "Married")
        //        {
        //            if (TotalDay != 3)
        //            {
        //                return 0;
        //            }
        //            else if (employee.MaritialStatus != "Single")
        //            {
        //                return 0;
        //            }
        //        }
        //        else if (requestVM.ReasonRequest == "Marry or Circumcise or Baptize Children" ||
        //            requestVM.ReasonRequest == "Wife gave birth or had a miscarriage" ||
        //            requestVM.ReasonRequest == "Husband or Wife Parents or In laws Children or Son In law have passed away")
        //        {
        //            if (TotalDay != 2)
        //            {
        //                return 0;
        //            }

        //        }
        //        else if (requestVM.ReasonRequest == "Family member in one house died")
        //        {
        //            if (TotalDay != 1)
        //            {
        //                return 0;
        //            }
        //        }

        //        var request = new Request()
        //        {
        //            Employee = employee,
        //            StartDate = requestVM.StartDate,
        //            EndDate = requestVM.EndDate,
        //            ReasonRequest = requestVM.ReasonRequest,
        //            Notes = requestVM.Notes,
        //            StatusRequest = "Waithing",
        //            LeaveCategory = requestVM.LeaveCategory
        //        };

        //        myContext.Add(request);
        //        Global.resRequest = myContext.SaveChanges();
        //        var sendEmail = new EmailRequest(myContext);
        //        sendEmail.SendRequestEmployee(employee, request.Id);
        //        sendEmail.SendRequestManager(manager, employee, request.Id);
        //        return 1;
        //    }
        //}

        //public int ApprovedManager(ApproveVM approveVM)
        //{
        //    var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
        //    //var emp_role = myContext.Roles.Where(e => e.RoleName == e.Employee.NIK).FirstOrDefault();
        //    //var hrd = myContext.Employees.Include(Role).Where(e => e.NIK == emp_role).FirstOrDefault();
        //    //var hrd = myContext.Employees.Include(EmployeeRole).Where(e => e.Employee.NIK == e.EmployeeRole."4101").FirstOrDefault();
        //    var hrd = myContext.Employees.Where(e => e.NIK.Contains("1111")).FirstOrDefault();

        //    var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();

        //    if (data == null)
        //    {
        //        return 0;
        //    }

        //    else if (data.StatusRequest == "Waithing")
        //    {
        //        data.StatusRequest = "Approved by Manager";
        //        myContext.Update(data);

        //        sendEmail.SendRequestHRD(hrd, employee, data.Id, approveVM.Notes);
        //    }

        //    else if (data.StatusRequest == "Approved by Manager" || data.StatusRequest == "Approved by HRD" || data.StatusRequest == "Reject by Manager" || data.StatusRequest == "Approved by HRD")
        //    {
        //        return 0;
        //    }

        //    else
        //    {
        //        return 0;
        //    }
        //    myContext.SaveChanges();
        //    return 1;
        //}

        //public int ApprovedHRD(ApproveVM approveVM /*, RequestVM requestVM*/)
        //{
        //    var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
        //    var hrd = myContext.Employees.Where(e => e.NIK.Contains("4401HRD")).FirstOrDefault();
        //    var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

        //    var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
        //    if (data == null)
        //    {
        //        return 0;
        //    }
        //    else if (data.StatusRequest == "Approved by HRD" || data.StatusRequest == "Reject by HRD" || data.StatusRequest == "Reject by Manager" || data.StatusRequest == "Waithing")
        //    {
        //        return 0;
        //    }
        //    else if (data.StatusRequest == "Approved by Manager")
        //    {

        //        data.StatusRequest = "Approved by HRD";
        //        if (data.ReasonRequest == "Married")
        //        {
        //            employee.MaritialStatus = "Married";
        //        }
        //        var TotalDay = (data.EndDate - data.StartDate).TotalDays + 1;
        //        if (data.LeaveCategory != "Special Request")
        //        {
        //            employee.RemainingQuota = (int)(employee.RemainingQuota - TotalDay);
        //        }
        //        myContext.Update(data);
        //        myContext.Update(employee);

        //        sendEmail.SendApproveHRD(manager, employee, data.Id, approveVM.Notes);
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //    myContext.SaveChanges();
        //    return 1;
        //}



        //public int Reject(ApproveVM approveVM)
        //{
        //    var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();


        //    var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
        //    if (data == null)
        //    {
        //        return 0;
        //    }
        //    else if (data.StatusRequest == "Reject by HRD" || data.StatusRequest == "Reject by Manager")
        //    {
        //        return 0;
        //    }

        //    else if (data.StatusRequest == "Waithing")
        //    {
        //        data.StatusRequest = "Reject by Manager";
        //        myContext.Update(data);

        //        sendEmail.SendReject(employee, data.Id, approveVM.Notes);
        //    }
        //    else if (data.StatusRequest == "Approved by Manager")
        //    {
        //        data.StatusRequest = "Reject by HRD";
        //        myContext.Update(data);

        //        sendEmail.SendReject(employee, data.Id, approveVM.Notes);
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //    myContext.SaveChanges();
        //    return 1;
        //}

        public List<GetDataHistory> RequestHistory()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("RejectManager", "Reject by Manager", DbType.String);
            dbparams.Add("ApproveHRD", "Approve by HRD", DbType.String);
            dbparams.Add("RejectHRD", "Reject by HRD", DbType.String);

            var result = Task.FromResult(dapper.GetAll<GetDataHistory>("[dbo].[SP_GetHistoryRequest]", dbparams,
                commandType: CommandType.StoredProcedure)).Result;
            return result.ToList();
        }

        public List<GetDataHistory> RequestActual()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Waiting", "Waiting", DbType.String);
            dbparams.Add("ApproveManager","Approve by Manager",  DbType.String);
            var result = Task.FromResult(dapper.GetAll<GetDataHistory>("[dbo].[SP_GetHistoryactual]", dbparams,
                commandType: CommandType.StoredProcedure)).Result;
            return result.ToList();
        }
        
        public List<Request> Getdataapprove(int Id)
        {
/*            var dbparams = new DynamicParameters();
            dbparams.Add("Id", Id);
            var result = Task.FromResult(dapper.Get<GetApproveVM>("[dbo].[SP_GetApproveData1]", dbparams,
                commandType: CommandType.StoredProcedure)).Result;*/
            var data = myContext.Requests.Include(a => a.Employee).Where(e => e.Id == Id).ToList();
            //var a = data.FirstOrDefault();
            return data;
        }

    }
}

