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

    public partial class FreeTime
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Время начала приёма")]
        public System.DateTime StartTime { get; set; }
        
        public virtual Doctor Doctor { get; set; }
    }
}
