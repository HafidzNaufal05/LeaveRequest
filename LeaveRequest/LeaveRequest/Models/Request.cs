using LeaveRequest.ViewModels;
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

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public LeaveCategory LeaveCategory { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string ReasonRequest { get; set; }
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Notes { get; set; }
        public StatusRequest StatusRequest { get; set; }
    }

    public enum StatusRequest
    {
        Waiting,
        ApprovedByManager,
        RejectByManager,
        ApprovedByHRD,
        RejectByHRD
    }

}
