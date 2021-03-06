﻿using System;
using System.Globalization;
using System.Threading;

namespace IfiNavetNotifier.Extentions
{
    public static class DateTimeExtentions
    {
        public static DateTime? toDate(this string dateTimeStr, string[] dateFmt)
        {
            // example: var dt = "2011-03-21 13:26".toDate(new string[]{"yyyy-MM-dd HH:mm", 
            //                                                  "M/d/yyyy h:mm:ss tt"});
            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
            if (dateFmt == null)
            {
                var dateInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                dateFmt = dateInfo.GetAllDateTimePatterns();
            }

            DateTime? result = null;
            if (DateTime.TryParseExact(dateTimeStr, dateFmt,
                CultureInfo.InvariantCulture, style, out var dt)) result = dt;
            return result;
        }

        public static DateTime? toDate(this string dateTimeStr, string dateFmt = null)
        {
            // example:   var dt="2011-03-21 13:26".toDate("yyyy-MM-dd HH:mm");
            // or simply  var dt="2011-03-21 13:26".toDate();        
            // call overloaded function with string array param
            var dateFmtArr = dateFmt == null ? null : new[] {dateFmt};
            return toDate(dateTimeStr, dateFmtArr);
        }
    }
}