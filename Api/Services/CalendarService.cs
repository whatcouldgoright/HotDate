using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hotdate.Services.Rules.Holidays;

namespace HotDate.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IEnumerable<IIsHolidayRule> _holidayRules;

        public CalendarService() {
            _holidayRules = LoadRules();
        }

        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
        public int GetBusinessDaysBetweenDates(DateTime fromDate, DateTime toDate) {

            if(fromDate.CompareTo(toDate) > 0)
            {
                throw new Exception("from date after to date");
            }

            fromDate = fromDate.Date.AddDays(1);
            toDate = toDate.Date;
            int businessDays = 0;
            
            var currentDate = fromDate;
            while(currentDate < toDate)
            {
                var isHoliday = false;

                foreach(IIsHolidayRule rule in _holidayRules)
                    isHoliday = (isHoliday || rule.IsHoliday(currentDate));
                
                if(!isHoliday)
                    businessDays++;

                currentDate = currentDate.AddDays(1);
            }
            return businessDays;
        }

        private IEnumerable<IIsHolidayRule> LoadRules() {

            var rules = new List<IIsHolidayRule>();

            Assembly thisAssembly = Assembly.GetAssembly(typeof(IIsHolidayRule));
            
            Type[] typesImplementRule = thisAssembly
                .GetTypes()
                .Where(t => typeof(IIsHolidayRule).IsAssignableFrom(t) && t.IsClass)
                .ToArray();

            foreach (Type rule in typesImplementRule) {
                IIsHolidayRule holidayRule = Activator.CreateInstance(rule) as IIsHolidayRule;
                rules.Add(holidayRule);
            }

            return rules;
        }

    }



    public class PublicHoliday {
        public string Name { get; set;}
        public int Month { get; set; }
        public int Day { get; set; }
        public bool Additional { get; set;}
        public int OffsetDirection { get; set;}
        public int OffsetDayOfWeekCondition { get; set;}
        public int OffsetCount { get; set;}
    }
}
