﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IEnumerable<Holiday> _holidays;

        public HolidayService() {
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

        private IEnumerable<Holiday> LoadHolidays() {

            List<Holiday> holidays = new List<Holiday>();
            holidays.Add(new Holiday { Name= "New Years Day", Month = 1, Date = 1} );
            holidays.Add(new Holiday { Name= "Australia Day", Month = 1, Date = 26} );
            return holidays;
        }

    }
}
