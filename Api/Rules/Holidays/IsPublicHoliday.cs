using System;
using System.Collections.Generic;

namespace Hotdate.Services.Rules.Holidays
{
    public class IsPublicHolidayRule: IIsHolidayRule {

        public List<DateTime> _publicHolidays {get; set;}

        public IsPublicHolidayRule() {
            _publicHolidays = new List<DateTime>();
            _publicHolidays.Add(DateTime.Parse("2021-01-26"));
        }

        public bool IsHoliday(DateTime date) {
            return _publicHolidays.Contains(date);
        }
    }
}
