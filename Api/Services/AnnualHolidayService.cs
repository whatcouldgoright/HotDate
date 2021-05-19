using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Services
{
    public class AnnualHolidayService : IHolidayService
    {
        private readonly IEnumerable<AnnualHoliday> _holidays;

        public AnnualHolidayService() {
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

        private IEnumerable<AnnualHoliday> LoadHolidays() {

            List<AnnualHoliday> holidays = new List<AnnualHoliday>();
            holidays.Add(new AnnualHoliday { Name= "New Years Day", Month = 1, Date = 1, Rollover = true } );
            holidays.Add(new AnnualHoliday { Name= "Australia Day", Month = 1, Date = 26} );
            return holidays;
        }

    }
}
