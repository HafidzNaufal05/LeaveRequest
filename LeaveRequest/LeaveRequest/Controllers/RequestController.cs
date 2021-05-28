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

namespace LeaveRequest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : BaseController<Request, RequestRepository, int>
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

        [HttpPost("SubmitApproved")]
        public ActionResult SubmitApproved(ApproveVM approveVM)
        {
            var data = requestRepository.ApprovedHRD(approveVM);
            if (data == 1)
            {
                return Ok(new { status = "Approved success" });
            }
            else
            {
                return StatusCode(500, new { status = "Internal Server Error" });
            }

        }

        //[HttpPut("SubmitReject")]
        //public ActionResult SubmitReject(ApproveVM approveRequestVM)
        //{
        //    var data = requestRepository.Reject(approveRequestVM);
        //    if (data == 1)
        //    {
        //        return Ok(new { status = "Reject success" });
        //    }
        //    else
        //    {
        //        return StatusCode(500, new { status = "Internal Server Error" });
        //    }

        //}

    }
}
