using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Base.Controller;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using Microsoft.Extensions.Configuration;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly EmployeeRepository employeeRepository;
        private IConfiguration configuration;

        public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.employeeRepository = employeeRepository;
            this.configuration = configuration;
        }
    }
}
