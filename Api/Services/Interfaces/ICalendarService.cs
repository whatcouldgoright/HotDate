using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hotdate.Services.Rules.Holidays;

namespace HotDate.Services
{
    public interface ICalendarService
    {
        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
        public int GetBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate);
    }
}
