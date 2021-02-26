using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRpsCrud_Elias.Models
{
    public class Instractor
    {
        public int InstractorID { get; set; }

        [Display(Name = "Instractor Name")]
        [Required(ErrorMessage = "Must Be Filled")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be 3-30 char")]
        public string InstractorName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Must Be Filled")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Must Be Filled")]
        public string CellPhone { get; set; }

        [Display(Name = "Permanent Address")]
        [Required(ErrorMessage = "Must Be Filled")]
        [DataType(DataType.MultilineText)]
        public string ContactAddress { get; set; }

        [Display(Name = "Joining Date")]
        [Required(ErrorMessage = "Must Be Filled")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]

        [DataType(DataType.DateTime)]
        //[CustomHireDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime JoiningDate { get; set; }


        [Required]
        [Display(Name = "Image")]
        public string ProfilePicture { get; set; }
    }
}
