﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequest.Repositories.Data
{
    public class ParameterRepository : GeneralRepository<Parameter, MyContext, string>
    {
        private readonly MyContext myContext;

        public ParameterRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}