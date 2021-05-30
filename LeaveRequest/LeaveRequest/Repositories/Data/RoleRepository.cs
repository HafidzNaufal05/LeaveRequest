using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequest.Repositories.Data
{
    public class RoleRepository : GeneralRepository<Role, MyContext, int>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        class Global
        {
            public static int resUpdate;

        }
        public int UpdateRole(UpdateRoleVM updateRoleVM)
        {
            var employee = myContext.Employees.Where(e => e.NIK == updateRoleVM.NIK).FirstOrDefault();
            var employeeRole = myContext.EmployeeRoles.Include(a =>a.Role).Where(e => e.Employee.NIK == updateRoleVM.NIK).FirstOrDefault();

            if (employeeRole == null)
            {
                return 0;
            }

            else
            {
                employeeRole.Role.Id = updateRoleVM.RoleId;
                myContext.Update(employeeRole);
            }
            Global.resUpdate = myContext.SaveChanges();
            return 1;
        }
    }
}
