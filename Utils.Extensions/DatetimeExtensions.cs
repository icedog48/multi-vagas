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
    }
}
