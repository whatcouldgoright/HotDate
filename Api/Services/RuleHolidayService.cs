using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Services
{
    public class RuleHolidayService : IHolidayService
    {
        private readonly IEnumerable<RuleHoliday> _holidays;

        public RuleHolidayService() {
            _holidays = LoadHolidays();
        }

        public bool IsHoliday(DateTime date)
        {
            return _holidays.Select(h => h.EffectiveDate(date.Year))
                            .Any(d => d.Equals(date));
        }

        public IEnumerable<IHoliday> GetHolidays(int year) {

            return _holidays;
        }

        private IEnumerable<RuleHoliday> LoadHolidays() {

            List<RuleHoliday> holidays = new List<RuleHoliday>();
            holidays.Add(new RuleHoliday { Name= "Queen's Birthday", Month = 6, Date = 1, DayOfWeek = 1, Occurence = 2 } );
            return holidays;
        }

    }
}
