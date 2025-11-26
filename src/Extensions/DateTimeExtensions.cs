using System;
using System.Collections.Generic;
using System.Text;

namespace WinKit.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFileSafeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd_HH-mm-ss");
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}
