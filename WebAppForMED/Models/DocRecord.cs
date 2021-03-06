//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppForMED.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DocRecord
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name ="Диагноз")]
        public string Diagnos { get; set; }

        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Время записи")]
        public System.DateTime RecordDate { get; set; }

        public virtual MedCard MedCard { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
