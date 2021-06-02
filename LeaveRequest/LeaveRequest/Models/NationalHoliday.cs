using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_M_NationalHoliday")]
    public class NationalHoliday
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Name { get; set; }
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }
    }
}
