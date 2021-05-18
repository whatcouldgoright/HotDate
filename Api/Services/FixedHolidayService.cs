using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Services
{
    public class FixedHolidayService : IHolidayService
    {
        private readonly IEnumerable<FixedHoliday> _holidays;

        public FixedHolidayService() {
            _holidays = LoadHolidays();
        }

        public bool IsHoliday(DateTime date)
        {
            return _holidays.Any(h => h.EffectiveDate(date.Year).Equals(date));
        }

        public IEnumerable<IHoliday> GetHolidays(int year) {

            return _holidays.Where(h => h.Year == year);
        }

        private IEnumerable<FixedHoliday> LoadHolidays() {

            List<FixedHoliday> holidays = new List<FixedHoliday>();
            holidays.Add(new FixedHoliday { Name= "Footy Day", Year = 2021, Month = 2, Date = 1} );
            return holidays;
        }

    }
}
