using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequest.Repositories.Data
{
    public class NationalHolidayRepository : GeneralRepository<NationalHoliday, MyContext, int>
    {
        private readonly MyContext myContext;

        public NationalHolidayRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
