using System;

namespace Hotdate.Services.Rules.Holidays
{
    public interface IIsHolidayRule {
        bool IsHoliday(DateTime date);
    }
}
