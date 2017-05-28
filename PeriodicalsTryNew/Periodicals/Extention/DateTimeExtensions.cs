using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Periodicals.Extention
{
    public static class DateTimeExtensions
    {
        public static string ToMonthName(this DateTime dateTime)
        {
            var cul = CultureInfo.CreateSpecificCulture("ru-RU");
            return cul.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
    }
}