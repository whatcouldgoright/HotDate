using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IEnumerable<IHolidayService> _holidayServices;

        public CalendarService(IEnumerable<IHolidayService> holidayServices) {
            _holidayServices = holidayServices;
        }

        public List<DateTime> GetHolidayDates(int year) {

            List<IHoliday> holidays = new List<IHoliday>();
            
            foreach(IHolidayService service in _holidayServices)
            {
                holidays.AddRange(service.GetHolidays(year));
            }
            holidays.RemoveAll(h => h == null);
            return holidays.Distinct().Select(h => h.EffectiveDate(year)).Distinct().ToList();
        }

        /// Fetches whole business days between two dates, exclusive of start and end date.  Simple iterator.
        public int GetBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate) {

            if(fromDate.CompareTo(toDate) > 0)
            {
                throw new ArgumentException("from date after to date");
            }

            fromDate = fromDate.Date.AddDays(1);
            toDate = toDate.Date;
            int businessDays = 0;
            
            var currentDate = fromDate;
            var holidayDates = GetHolidayDates(currentDate.Year);
            var fetchedHolidayYear = currentDate.Year;
            while(currentDate < toDate)
            {
                if(fetchedHolidayYear != currentDate.Year)
                    holidayDates = GetHolidayDates(currentDate.Year);

                var isWeekend = (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday);
                var isHoliday = holidayDates.Contains(currentDate);

                if(!isWeekend && !isHoliday)
                    businessDays++;

                currentDate = currentDate.AddDays(1);
            }
            return businessDays;
        }

        // Process business days faster by:
        //  - reducing iteration
        //  - simplifying weekend count to 2 per week, accounting for head and tail
        //  - assume only non-weekend effective holidays need to be counted, and deduct total
        // Known issue: finds an extra day compared to "slow" 
        public int FastBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate) {

            if(fromDate.CompareTo(toDate) > 0)
            {
                throw new ArgumentException("from date after to date");
            }

            fromDate = fromDate.Date.AddDays(1);
            toDate = toDate.Date;
            var businessDays = 0;

            // chunk by year to apply holiday rules
            for(int y = fromDate.Year; y <= toDate.AddDays(-1).Year; y++)
            {
                DateTime chunkStart = (y == fromDate.Year) ? fromDate 
                                                           : new DateTime(y, 1, 1);
                DateTime chunkEnd = (y == toDate.Year) ? toDate.AddDays(-1)        // don't check to date
                                                       : new DateTime(y+1, 1, 1);  // but do check last day in chunk

                // count business days by week
                var chunkBusinessDays = 0;
                var blockDays = (chunkEnd - chunkStart).Days;
                var weeks = blockDays / 7;
                var remainder = blockDays % 7;
                chunkBusinessDays = chunkBusinessDays + (weeks * 5);

                // check remainder dates iteratively
                var tailDate = chunkEnd;
                while(remainder > 0)
                {   
                    if(tailDate.DayOfWeek != DayOfWeek.Saturday && tailDate.DayOfWeek != DayOfWeek.Sunday)
                        chunkBusinessDays++;
                    
                    tailDate = tailDate.AddDays(-1);
                    remainder --;
                }

                // subtract days for any holidays effective on weekdays
                var chunkHolidays = GetHolidayDates(y).Where(hd => hd >= chunkStart && hd <= chunkEnd)
                                                      .Where(hd => hd.DayOfWeek != DayOfWeek.Saturday && hd.DayOfWeek != DayOfWeek.Sunday)
                                                      .Count();
                chunkBusinessDays = chunkBusinessDays - chunkHolidays;

                businessDays += chunkBusinessDays;
            }
            return businessDays;
        }

    }

}
