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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : BaseController<Request, RequestRepository, int>
    {
        private readonly RequestRepository requestRepository;
        public RequestController(RequestRepository requestRepository) : base(requestRepository)
        {
            this.requestRepository = requestRepository;
        }

        [HttpPost("RequestCuti")]
        public ActionResult RequestCuti(RequestVM requestVM)
        {
            var data = requestRepository.Request(requestVM);
            if (data >= 1)
            {
                return Ok("Request Has Been Add");
            }
            return NotFound("Request Not Add");

        }

        [HttpPost("SubmitApprovedManager")]
        public ActionResult SubmitApprovedManager(ApproveVM approveVM)
        {
            var data = requestRepository.ApprovedManager(approveVM);
            if (data >= 1)
            {
                return Ok(new { status = "Approved success" });
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
            }

        }

        [HttpPost("SubmitApprovedHRD")]
        public ActionResult SubmitApprovedHRD(ApproveVM approveVM)
        {
            var data = requestRepository.ApprovedHRD(approveVM);
            if (data >= 1)
            {
                return Ok(new { status = "Approved success" });
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
                }

        }

        [HttpPut("SubmitReject")]
        public ActionResult SubmitRejectManager(ApproveVM approveRequestVM)
        {
            var data = requestRepository.Reject(approveRequestVM);
            if (data >= 1)
            {
                return Ok(new { status = "Reject success" });
            }
            else
            {
                return StatusCode(404, new { status = "Data Not Found" });
            }

        }

    }
}
