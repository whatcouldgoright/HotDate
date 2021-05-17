using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hotdate.Services.Rules.Holidays;

namespace HotDate.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IEnumerable<PublicHoliday> _holidays;

        public HolidayService() {
            _holidays = LoadHolidays();
        }

        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
        public IEnumerable<PublicHoliday> GetPublicHolidays() {

            return _holidays;
        }

        private IEnumerable<PublicHoliday> LoadHolidays() {

            List<PublicHoliday> holidays = new List<PublicHoliday>();
            holidays.Add(new PublicHoliday { Name= "New Years Day", Month = 1, Day = 1, Additional = true} );
            holidays.Add(new PublicHoliday { Name= "Australia Day", Month = 1, Day = 26, Additional = false} );
            return holidays;
        }

    }
}
