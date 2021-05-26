using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Context
{
    [Table("TB_M_Parameter")]
    public class Paramater
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Notes { get; set; }
    }
}