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

        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
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
            while(currentDate < toDate)
            {
                var isHoliday = false;
                
                isHoliday = isHoliday || (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday);
                isHoliday = isHoliday || holidayDates.Contains(currentDate);

                if(!isHoliday)
                    businessDays++;

                if(currentDate.AddDays(1).Year > currentDate.Year)
                    holidayDates = GetHolidayDates(currentDate.Year);
                    
                currentDate = currentDate.AddDays(1);
            }
            return businessDays;
        }

        public int FastBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate) {

            if(fromDate.CompareTo(toDate) > 0)
            {
                throw new ArgumentException("from date after to date");
            }

            fromDate = fromDate.Date.AddDays(1);
            toDate = toDate.Date;
            
            var currentDate = fromDate;

            var holidayDates = GetHolidayDates(currentDate.Year).Distinct();

            var start = DateTime.Parse("2021-01-01");
            var end = DateTime.Parse("2021-12-31");

            var businessDays = (end - start).Days;

            var weekends = businessDays / 7 * 2;
            // HS TODO: account for head and tail overruns
            var weekdayHolidays = holidayDates.Where(h => h.DayOfWeek != DayOfWeek.Saturday && h.DayOfWeek != DayOfWeek.Sunday);

            businessDays -= weekends;
            businessDays -= weekdayHolidays.Count();

            return businessDays;
        }
        

    }

}
