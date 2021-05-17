using System;

namespace Hotdate.Services.Rules.Holidays
{
    public class IsWeekendRule: IIsHolidayRule {
        public bool IsHoliday(DateTime date) {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }
    }
}
