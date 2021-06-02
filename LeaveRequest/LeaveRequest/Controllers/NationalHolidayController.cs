using LeaveRequest.Base.Controller;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class NationalHolidayController : BaseController<NationalHoliday, NationalHolidayRepository, int>
    {
        public NationalHolidayController(NationalHolidayRepository nationalHolidayRepository) : base(nationalHolidayRepository)
        {

        }
    }
}
