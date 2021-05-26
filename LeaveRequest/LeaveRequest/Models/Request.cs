using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_T_Request")]
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonRequest { get; set; }
        public Employee Employee { get; set; }
        public string Notes { get; set; }
        public StatusRequest StatusRequest { get; set; }
    }

    public enum StatusRequest
    {
        Waiting,
        ApprovedByHRD,
        RejectByHRD,
        ApprovedByManager,
        RejectByManager
    }
}
