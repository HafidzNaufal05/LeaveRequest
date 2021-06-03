using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class GetDataHistory
    {
        public string NIK { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), MaxLength(30, ErrorMessage = "Maksimal 30 karakter"), RegularExpression(@"^\D+$", ErrorMessage = "Tidak boleh berupa angka")]
        public string FirstName { get; set; }
        [MaxLength(255, ErrorMessage = "Maksimal 255 karakter"), RegularExpression(@"^\D+$", ErrorMessage = "Tidak boleh berupa angka")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Gender { get; set; }
        [MaxLength(255, ErrorMessage = "Maksimal 255 karakter")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string MaritialStatus { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), RegularExpression(@"^08[0-9]{10,11}$", ErrorMessage = "Harus berupa angka diawali 08"), MinLength(11, ErrorMessage = "Minimal 11 karakter"), MaxLength(12, ErrorMessage = "Maksimal 12 karakter")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), EmailAddress(ErrorMessage = "Masukan format email yang valid"), MaxLength(255, ErrorMessage = "Maksimal 255 karakter")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime JoinDate { get; set; }
        public int RemainingQuota { get; set; }

#nullable enable
        public string? NIK_Manager { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string LeaveCategory { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string ReasonRequest { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Notes { get; set; }
        public string StatusRequest { get; set; }
    }
}
