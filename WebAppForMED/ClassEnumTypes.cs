using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebAppForMED
{
    public static class ClassEnumTypes
    {
        static readonly string[] docTypes = { "Паспорт гражданина РФ", "Удостоверение беженца", "Военный билет", "Временное удостоверение личности" };
        static readonly string[] polTypes = { "Мужской", "Женский" };
        static readonly string[] bloodTypes = { "1+", "2", "3+", "4+", "1-", "2-", "3-", "4-" };

        public static SelectList DocTypes = new SelectList(docTypes);
        public static SelectList PolTypes = new SelectList(polTypes);
        public static SelectList BloodTypes = new SelectList(bloodTypes);
    }
}