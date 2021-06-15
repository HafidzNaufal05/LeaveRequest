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
using System.IdentityModel.Tokens.Jwt;

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

        [HttpPost("RequestCuti")] 
        public ActionResult RequestCuti(RequestVM requestVM)
        {
            //try
            //{
            var employee = myContext.Employees.Where(e => e.NIK == requestVM.EmployeeNIK).FirstOrDefault();
            var manager = myContext.Employees.Where(e => e.NIK == employee.NIK_Manager).FirstOrDefault();
            var request = new Request();

            var dbparams = new DynamicParameters();
            dbparams.Add("EmployeeNIK", requestVM.EmployeeNIK, DbType.String);
            //dbparams.Add("LeaveCategory", requestVM.LeaveCategory, DbType.String);
            dbparams.Add("StartDate", requestVM.StartDate, DbType.Date);
            dbparams.Add("EndDate", requestVM.EndDate, DbType.Date);
            dbparams.Add("ReasonRequest", requestVM.ReasonRequest, DbType.String);
            dbparams.Add("Notes", requestVM.Notes, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_Request]", dbparams, commandType: CommandType.StoredProcedure)).Result;
            if (result == 1)
            {
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendRequestEmployee(employee, requestVM);
                sendEmail.SendRequestManager(manager, employee/*, request.Id*/, requestVM);
                return Ok(new { Status = "Success", Message = "Request Has Been Add" });
            }
            else
            {
                return NotFound(new { Status = "Not Found", Message = "Gagal Add Data" });
            }
              
        }

        [HttpPost("SubmitApprovedManager")]
        public ActionResult SubmitApprovedManager(ApproveVM approveVM)
        {

            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("1128")).FirstOrDefault();

            var data = myContext.Requests.Where(e => e.Id == approveVM.Id).FirstOrDefault();
            

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
                
                var sendEmail = new EmailRequest(myContext);
                sendEmail.SendRequestHRD(hrd, employee, data.Id, data, approveVM.Notes);
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
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("1128")).FirstOrDefault();
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
                sendEmail.SendApproveHRD(manager, employee, data, approveVM.Notes);
                return Ok(new { Status = "Success", Message = "HRD, Your Approved success" });
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = "Error" });
            }
        }

        [HttpPost("SubmitApprovedHRD2")]
        public ActionResult SubmitApprovedHRD2(ApproveVM approveVM)
        {
            var employee = myContext.Employees.Where(e => e.Email == approveVM.Email).FirstOrDefault();
            var hrd = myContext.Employees.Where(e => e.NIK.Contains("1128")).FirstOrDefault();
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
                sendEmail.SendApproveHRD(manager, employee, data, approveVM.Notes);
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

        [HttpGet("GetRequestData")]
        public List<dynamic> GetRequestData()
        {
            string query = string.Format("SELECT * From TB_T_Request AS R FULL JOIN TB_M_Employee AS E ON R.EmployeeNIK = E.NIK WHERE(R.StatusRequest = 'Waiting') OR(R.StatusRequest = 'Approved by Manager')");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
        }

        [HttpGet("GetApproveDataManager")]
        public List<dynamic> GetApproveDataManager()
        {
            string query = string.Format("SELECT req.EmployeeNIK AS NIK,emp.Email, CONCAT(emp.FirstName,' ',emp.LastName) AS FullName, req.Id, req.LeaveCategory, req.StartDate, req.EndDate, req.ReasonRequest, req.Notes, req.StatusRequest FROM TB_M_Employee AS emp INNER JOIN TB_T_Request AS req ON req.EmployeeNIK = emp.NIK WHERE (req.StatusRequest = 'Waiting') OR (req.StatusRequest = 'Approved by Manager') OR (req.StatusRequest = 'Reject by Manager')");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
        }

        [HttpGet("GetApproveData")]
        public List<dynamic> GetApproveData()
        {
            string query = string.Format("SELECT req.EmployeeNIK AS NIK,emp.Email, CONCAT(emp.FirstName,' ',emp.LastName) AS FullName, req.Id, req.LeaveCategory, req.StartDate, req.EndDate, req.ReasonRequest, req.Notes, req.StatusRequest FROM TB_M_Employee AS emp INNER JOIN TB_T_Request AS req ON req.EmployeeNIK = emp.NIK WHERE (req.StatusRequest = 'Approved by Manager') OR (req.StatusRequest = 'Approved by HRD') OR (req.StatusRequest = 'Reject by HRD')");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
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

        [HttpGet("Getdataapprove")]
        public ActionResult Getdataapprove(int Id)
        {
            var data = requestRepository.Getdataapprove(Id);
            if (data.Count() >= 1)
            {
                return Ok(data);
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
            }
        }

        [HttpGet("GetHistoryRequest3")]
        public List<dynamic> GetHistoryRequest3()
        {
            //string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            //var jwtReader = new JwtSecurityTokenHandler();
            //var jwt = jwtReader.ReadJwtToken(token);

            //var NIK = jwt.Claims.First(t => t.Type == "unique_name").Value;

            //var getEmployee = myContext.Employees.Where(e => e.NIK == NIK).FirstOrDefault();

            //var dbprams = new DynamicParameters();
            //dbprams.Add("NIK", getVM.NIK, DbType.String);
            string query = string.Format("SELECT E.NIK, R.*, E.RemainingQuota From TB_T_Request AS R INNER JOIN TB_M_Employee AS E ON R.EmployeeNIK = E.NIK WHERE (R.StatusRequest = 'Reject by Manager') OR (R.StatusRequest = 'Approved by HRD') OR (R.StatusRequest = 'Reject by HRD')");

            List<dynamic> result = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return result;
        }
    }
}
