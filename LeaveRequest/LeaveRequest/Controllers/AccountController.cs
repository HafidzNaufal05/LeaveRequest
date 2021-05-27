using Dapper;
using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.Repositories.Interfaces;
using LeaveRequest.Services;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository accountRepository;
        private ParameterRepository parameterRepository;
        private MyContext myContext;
        private IConfiguration configuration;
        private readonly IGenericDapper dapper;

        public AccountController(IConfiguration configuration, AccountRepository accountRepository, MyContext myContext, IGenericDapper dapper, ParameterRepository parameterRepository)
        {
            this.configuration = configuration;
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this.dapper = dapper;
            this.parameterRepository = parameterRepository;
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Registration(RegisterVM registerVM)
        {
            var HashPassword = Hashing.HashPassword(registerVM.Password);

            DateTime DateJoin = registerVM.JoinDate;
            DateTime Today = DateTime.Today;
            TimeSpan ts = new TimeSpan();
            ts = Today.Subtract(DateJoin);
            Parameter parameter = new Parameter();
            if (ts.Days < 365)
            {
                parameter = parameterRepository.getByName("Dibawah satu tahun");
            }
            else if (ts.Days >= 365 && ts.Days < 1826)
            {
                parameter = parameterRepository.getByName("Diatas satu tahun");
            }
            else if (ts.Days >= 1826)
            {
                parameter = parameterRepository.getByName("Diatas lima tahun");
            }

            var dbparams = new DynamicParameters();
            dbparams.Add("NIK", registerVM.NIK, DbType.String);
            dbparams.Add("FirstName", registerVM.FirstName, DbType.String);
            dbparams.Add("LastName", registerVM.LastName, DbType.String);
            dbparams.Add("BirthDate", registerVM.BirthDate, DbType.DateTime);
            dbparams.Add("Gender", registerVM.Gender, DbType.String);
            dbparams.Add("Address", registerVM.Address, DbType.String);
            dbparams.Add("MaritialStatus", registerVM.MaritialStatus, DbType.String);
            dbparams.Add("PhoneNumber", registerVM.PhoneNumber, DbType.String);
            dbparams.Add("Email", registerVM.Email, DbType.String);
            dbparams.Add("JoinDate", registerVM.JoinDate, DbType.DateTime);
            dbparams.Add("RemainingQuota", parameter.Value, DbType.Int32);
            dbparams.Add("DepartmentId", registerVM.DepartmentId, DbType.String);
            dbparams.Add("NIK_Manager", registerVM.NIK_Manager, DbType.String);
            dbparams.Add("Role", registerVM.RoleId, DbType.String);
            dbparams.Add("Password", HashPassword, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_Register]", dbparams, commandType: CommandType.StoredProcedure));
            return Ok(new { Status = "Success", Message = "User has been registered seccessfully" });
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Email", loginVM.Email, DbType.String);
            var result = Task.FromResult(dapper.Get<dynamic>("[dbo].[SP_Login]", dbparams,
                commandType: CommandType.StoredProcedure)).Result;

            if (Hashing.ValidatePassword(loginVM.Password, result.Password))
            {
                var jwt = new JwtService(configuration);
                var token = jwt.GenerateSecurityToken(result.Name, result.Email, result.Role);
                return Ok(new { token });
            }

            return BadRequest("Failed to login");

        }

        /*[HttpPost("ChangePassword")]
        public ActionResult ChangePassword(string email, string oldPassword, string newPassword, string confrimPassword)
        {
            var emailCheck = myContext.Accounts.SingleOrDefault(e => e.Employee.Email == email);
            var passwordCheck = Hashing.ValidatePassword(oldPassword, emailCheck.Password);

            if (emailCheck != null)
            {
                if (passwordCheck)
                {
                    var NewPassword = Hashing.HashPassword(newPassword);
                    emailCheck.Password = NewPassword;
                    var Save = myContext.SaveChanges();
                    if(Save > 0)
                    {
                        return Ok(new { message = "Password Changed", status = "Ok" });
                    }
                }
                else
                {
                    return StatusCode(404, new { status = "404", message = "Wrong password" });
                }
            }
            return NotFound();
        }*/

        //[HttpPost("ForgotPassword")]
        //public ActionResult ForgotPassword(string email)
        //{
        //    string resetCode = Guid.NewGuid().ToString();
            
        //    var acc = myContext.Employees.Include(u => u.Account).Where(a => a.Email == email).FirstOrDefault();
        //    if (acc.Email == email)
        //    {
        //        var password = Hashing.HashPassword(resetCode);
        //        acc.Account.Password = password;
        //        var result = myContext.SaveChanges();
        //        sendEmail.
        //        sendEmail.SendForgotPassword2(resetCode, email);
        //        return result;
        //        /*var sendEmail = new SendEmail(myContext);
        //        sendEmail.SendForgotPassword(email);
        //        return Ok("Please Check Your Email");*/
        //    }
        //    else
        //    {
        //        return NotFound("Email Not Found");
        //    }
        //}

        [HttpPost("ResetPassword")]
        //[Authorize]
        public ActionResult ResetPassword(string email, string newPassword, string confirmPassword)
        {
            var acc = myContext.Employees.SingleOrDefault(e => e.Email == email);
            var password = myContext.Accounts.SingleOrDefault(a => a.Employee.Email == email);
            if (acc.Email == email)
            {
                if (newPassword == confirmPassword)
                {
                    password.Password = Hashing.HashPassword(newPassword);
                    var save = myContext.SaveChanges();
                    if (save > 0)
                    {
                        return Ok("Password Berhasil Dirubah");
                    }
                }
                else
                {
                    return BadRequest("Your Password is incorrect");
                }
            }

            return NotFound("Email Not Found");

        }
    }
}
