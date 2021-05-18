using System;
using System.Collections.Generic;

namespace HotDate.Services
{
    public interface ICalendarService
    {
        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
        int GetBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate);
        List<DateTime> GetHolidayDates(int year);
    }
}
