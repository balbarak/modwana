using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Modwana.Core.Extensions
{
    public static class DateExtensions
    {
        public static string ToSystemDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public static string ToSystemDateTime(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy @ hh:mm tt");
        }

        public static string ToHijriDate(this DateTime date, string format = null)
        {
            return date.ToString(format ?? $"dd-MM-yyyy", new CultureInfo("ar-sa"));
        }
    }
}
