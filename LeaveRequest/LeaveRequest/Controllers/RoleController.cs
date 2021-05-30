using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository roleRepository;
        public RoleController(RoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpPut("AdminUpdateRole")]
        public ActionResult UpdateRole(UpdateRoleVM updateRole)
        {
            var data = roleRepository.UpdateRole(updateRole);
            if (data >= 1)
            {
                return Ok("Request Has Been Add");
            }
            return NotFound("Request Not Add");

        }
    }
}
