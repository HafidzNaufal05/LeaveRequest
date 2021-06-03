using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class RequestVM
    {
        //public int Id { get; set; }
        public string EmployeeNIK { get; set; }
        public string LeaveCategory { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }
        public string ReasonRequest { get; set; }
        public string Notes { get; set; }
        //public int RemainingQuota { get; set; }
    }
    //public enum LeaveCategory
    //{
    //    SpecialRequest,
    //    NormalRequest
    //}
}
