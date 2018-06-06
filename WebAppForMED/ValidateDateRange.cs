using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebAppForMED
{
    public class ValidateDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if ((DateTime)value >= Convert.ToDateTime("01/01/1900") && (DateTime)value <= Convert.ToDateTime("01/12/2020"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Дата должна быть в пределах от 01/01/1900 до 01/12/2020");
            }
        }
    }
}