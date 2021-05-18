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
            while(currentDate < toDate)
            {
                var isHoliday = false;

                
                if(!isHoliday)
                    businessDays++;

                currentDate = currentDate.AddDays(1);
            }
            return businessDays;
        }

    }

}
