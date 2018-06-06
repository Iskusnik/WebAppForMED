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


    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.WorkTime = new HashSet<WorkTime>();
        }
    
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"(([А-Я][а-я]* )([А-Я][а-я]*)){1}( [А-Я][а-я]*){0,1}$", ErrorMessage = "Некорректные ФИО")]
        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"([А-Я][а-я]*)$", ErrorMessage = "Должно быть: с большой буквы, без цифр")]
        [Display(Name = "Гражданство")]
        public string Nation { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [ValidateDateRange]
        public System.DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Место рождения")]
        public string BirthPlace { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Место жительства")]
        public string LivePlace { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Пол")]
        public string Pol { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"([0-9]+)$", ErrorMessage = "Должно быть: только цифры")]
        [Display(Name = "Номер ОМС")]
        public string OMS { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Группа крови")]
        public string Blood { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Тип документа")]
        public string DocType { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"([0-9]+)$", ErrorMessage = "Должно быть: только цифры")]
        [Display(Name = "Номер документа")]
        public string DocNum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkTime> WorkTime { get; set; }
        public virtual MedCard MedCard { get; set; }
    }
}