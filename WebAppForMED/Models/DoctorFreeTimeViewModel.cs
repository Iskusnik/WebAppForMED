using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebAppForMED.Models
{
    public class DoctorFreeTimeViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Врач")]
        public int DoctorId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Время приёма")]
        public int FreeTimeId { get; set; }
        public int PatientId { get; set; }


        [Display(Name = "Должность")]
        public string Job { get; set; }
    }
}