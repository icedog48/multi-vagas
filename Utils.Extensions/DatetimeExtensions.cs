using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Round(this DateTime dateTime)
        {
            DateTime result = dateTime.AddMinutes(dateTime.Minute >= 30 ? (60 - dateTime.Minute) : (30 - dateTime.Minute));
            result = result.AddSeconds(-1 * result.Second); // To reset seconds to 0
            result = result.AddMilliseconds(-1 * result.Millisecond); // To reset milliseconds to 0

            return result;
        }

        /// <summary>
        /// returns the number of milliseconds since Jan 1, 1970 (useful for converting C# dates to JS dates)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double UnixTicks(this DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }
    }
}
