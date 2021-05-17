using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hotdate.Services.Rules.Holidays;

namespace HotDate.Services
{
    public interface IHolidayService
    {
        /*
         * Fetches whole business days between two dates, exclusive of start and end date.
         */
        public IEnumerable<PublicHoliday> GetPublicHolidays();
    }
}
