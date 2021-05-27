using LeaveRequest.Base.Controller;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.ViewModels;
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
    public class RequestController : BaseController<Request, RequestRepository, string>
    {
        private readonly RequestRepository requestRepository;
        public RequestController(RequestRepository requestRepository) : base(requestRepository)
        {
            this.requestRepository = requestRepository;
        }

        [HttpPost("requestCuti")]
        public ActionResult RequestCuti(RequestVM requestVM)
        {
            return Ok(requestRepository.Request(requestVM));
        }

    }
}
