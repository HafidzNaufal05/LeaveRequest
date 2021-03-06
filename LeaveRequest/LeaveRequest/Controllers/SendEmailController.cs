using LeaveRequest.Base.Controller;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : BaseController<SendEmail, SendEmailRepository, int>
    {
        public SendEmailController(SendEmailRepository sendEmailRepository) : base(sendEmailRepository)
        {
        }
    }
}
