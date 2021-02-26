using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreRpsCrud_Elias.Models
{
    public class Admission
    {
        [Key]
        public int AdmissionId { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        [DisplayName("Trainee ID")]
        [Required(ErrorMessage = "Must be filled")]
        [MaxLength(12, ErrorMessage = "Maximum 12 characters only")]
        public string TraineeID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Course Name")]
        [Required(ErrorMessage = "Must Be Filled")]
        public string CourseName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Instractor Name")]
        [Required(ErrorMessage = "Must Be Filled")]
        public string InstractorName { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("Voucher No :")]
        [Required(ErrorMessage = "Must Be Filled")]
        [MaxLength(11)]
        public string TransanctionId { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "must be filled amount.")]
        public int CourseFee { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Admission Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AdmissionDate { get; set; }


     
    }
}
