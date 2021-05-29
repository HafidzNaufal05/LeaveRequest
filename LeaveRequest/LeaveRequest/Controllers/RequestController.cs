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

        [HttpPost("requestCuti")]
        public ActionResult RequestCuti(RequestVM requestVM)
        {
            return Ok(requestRepository.Request(requestVM));
        }

        [HttpPost("SubmitApproved")]
        public ActionResult SubmitApproved(ApproveVM approveVM)
        {
            if (approveVM.Role == 3) //manager
            {
                var dataManager = requestRepository.ApprovalManager(approveVM);
                if (dataManager == 1)
                {
                    return Ok(new { status = "Approved success" });
                }
                else
                {
                    return StatusCode(500, new { status = "Internal Server Error" });
                }
            }
            else if(approveVM.Role == 4) //HRD
            {
                var dataHRD = requestRepository.ApprovalHRD(approveVM);
                if (dataHRD == 1)
                {
                    return Ok(new { status = "Approved success" });
                }
                else
                {
                    return StatusCode(500, new { status = "Internal Server Error" });
                }
            }
            else
            {
                return StatusCode(500, new { status = "Your role not permit to do this action" });
            }
        }

        [HttpPost("RejectApproved")]
        public ActionResult RejectApproved(ApproveVM approveVM)
        {
            if (approveVM.Role == 3) //manager
            {
                var dataManager = requestRepository.RejectManager(approveVM);
                if (dataManager == 1)
                {
                    return Ok(new { status = "Rejected success" });
                }
                else
                {
                    return StatusCode(500, new { status = "Internal Server Error" });
                }
            }
            else if (approveVM.Role == 4) //HRD
            {
                var dataHRD = requestRepository.RejectHRD(approveVM);
                if (dataHRD == 1)
                {
                    return Ok(new { status = "Rejected success" });
                }
                else
                {
                    return StatusCode(500, new { status = "Internal Server Error" });
                }
            }
            else
            {
                return StatusCode(500, new { status = "Your role not permit to do this action" });
            }
        }
    }
}
