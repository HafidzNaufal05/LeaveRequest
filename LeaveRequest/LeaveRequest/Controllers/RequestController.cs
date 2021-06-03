using LeaveRequest.Base.Controller;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using Microsoft.Extensions.Configuration;
using LeaveRequest.Repositories.Interfaces;
using System.Data;
using Dapper;
using LeaveRequest.Handler;

namespace LeaveRequest.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : BaseController<Request, RequestRepository, int>
    {
        private readonly RequestRepository requestRepository;
        private MyContext myContext;
        private IConfiguration configuration;
        private readonly IGenericDapper dapper;

        public RequestController(RequestRepository requestRepository, MyContext myContext, IConfiguration configuration, IGenericDapper dapper) : base(requestRepository)
        {
            this.requestRepository = requestRepository;
            this.configuration = configuration;
            this.myContext = myContext;
            this.dapper = dapper;
        }

        [HttpPost("Request")]
        public ActionResult RequestCuti(RequestVM requestVM)
        {
            var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();
            var request = new Request();

            var dbparams = new DynamicParameters();
            dbparams.Add("EmployeeNIK", requestVM.EmployeeNIK, DbType.String);
            dbparams.Add("LeaveCategory", requestVM.LeaveCategory, DbType.String);
            dbparams.Add("StartDate", requestVM.StartDate, DbType.Date);
            dbparams.Add("EndDate", requestVM.EndDate, DbType.Date);
            dbparams.Add("ReasonRequest", requestVM.ReasonRequest, DbType.String);
            dbparams.Add("Notes", requestVM.Notes, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_Request]", dbparams, commandType: CommandType.StoredProcedure));
            var sendEmail = new EmailRequest(myContext);
            sendEmail.SendRequestEmployee(employee, request.Id);
            sendEmail.SendRequestManager(manager, employee, request.Id);
            return Ok(new { Status = "Success", Message = "Request Has Been Add" });
        }

        [HttpPost("SubmitApprovedManager")]
        public ActionResult SubmitApprovedManager(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("1111")).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            var sendEmail = new EmailRequest(myContext);

            if (data == null)
            {
                return BadRequest(new { Status = "Error", Message = "Data is Null" });
            }

            else if (data.StatusRequest == "Waiting")
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Id", approveVM.Id, DbType.Int32);
                dbparams.Add("Email", approveVM.Email, DbType.String);
                dbparams.Add("Notes", approveVM.Notes, DbType.String);

                var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_ApprovedManager]", dbparams, commandType: CommandType.StoredProcedure));

                sendEmail.SendRequestHRD(hrd, employee, data.Id, approveVM.Notes);
                return Ok(new { Status = "Success", Message = "Manager, Your Approved success" });
            }

            else if (data.StatusRequest == "Approved by Manager" || data.StatusRequest == "Approved by HRD" || data.StatusRequest == "Reject by Manager" || data.StatusRequest == "Approved by HRD")
            {
                return BadRequest(new { Status = "Error", Message = "You are unstructured" });
            }

            else
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
        }

        [HttpPost("SubmitApprovedHRD")]
        public ActionResult SubmitApprovedHRD(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("4401HRD")).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return BadRequest(new { Status = "Error", Message = "Data is Null" });
            }
            else if (data.StatusRequest == "Approved by HRD" || data.StatusRequest == "Reject by HRD" || data.StatusRequest == "Reject by Manager" || data.StatusRequest == "Waiting")
            {
                return BadRequest(new { Status = "Error", Message = "You are unstructured" });
            }
            else if (data.StatusRequest == "Approved by Manager")
            {

                var dbparams = new DynamicParameters();
                dbparams.Add("Id", approveVM.Id, DbType.Int32);
                dbparams.Add("Email", approveVM.Email, DbType.String);
                dbparams.Add("Notes", approveVM.Notes, DbType.String);

                var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_ApprovedHRD]", dbparams, commandType: CommandType.StoredProcedure));
                
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendApproveHRD(manager, employee, data.Id, approveVM.Notes);
                return Ok(new { Status = "Success", Message = "HRD, Your Approved success" });
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
        }
    
        
        [HttpPut("SubmitRejectManager")]
        public ActionResult SubmitRejectManager(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();


            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
            else if (data.StatusRequest == "Reject by HRD" || data.StatusRequest == "Reject by Manager" || data.StatusRequest == "Approved by Manager")
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }

            else if (data.StatusRequest == "Waiting")
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Id", approveVM.Id, DbType.Int32);
                dbparams.Add("Email", approveVM.Email, DbType.String);
                dbparams.Add("Notes", approveVM.Notes, DbType.String);

                var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_RejectManager]", dbparams, commandType: CommandType.StoredProcedure));
                
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendReject(employee, data.Id, approveVM.Notes);
                return Ok(new { Status = "Success", Message = "Reject success" });
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }

        }        
        
        [HttpPut("SubmitRejectHRD")]
        public ActionResult SubmitRejectHRD(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();


            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            if (data == null)
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
            else if (data.StatusRequest == "Waiting" || data.StatusRequest == "Reject by HRD" || data.StatusRequest == "Reject by Manager")
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
            else if (data.StatusRequest == "Approved by Manager")
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Id", approveVM.Id, DbType.Int32);
                dbparams.Add("Email", approveVM.Email, DbType.String);
                dbparams.Add("Notes", approveVM.Notes, DbType.String);

                var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_RejectHRD]", dbparams, commandType: CommandType.StoredProcedure));
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendReject(employee, data.Id, approveVM.Notes);
                return Ok(new { Status = "Success", Message = "Reject success" });
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
        }

        [HttpGet("RequestHistory")]
        public ActionResult GetRequestHistory()
        {
            var data = requestRepository.RequestHistory();
            if (data.Count() >= 1)
            {
                return Ok(data);
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
            }
        }

        [HttpGet("RequestActual")]
        public ActionResult GetRequestActual()
        {
            var data = requestRepository.RequestActual();
            if (data.Count() >= 1)
            {
                return Ok(data);
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
            }
        }

    }
}
