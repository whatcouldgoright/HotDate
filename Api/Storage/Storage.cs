using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;

namespace HotDate.Storage
{
    public class Storage : IStorage
    {
        public List<AnnualHoliday> _annualHolidays;
        public List<AdHocHoliday> _adhocHolidays;

        public Storage() {
            _adhocHolidays = new List<AdHocHoliday>();
            _adhocHolidays.Add(new AdHocHoliday { Name= "Footy Day", Year = 2021, Month = 2, Date = 1} );
        }

        public List<AdHocHoliday> GetAdHocHolidays()
        {
            return _adhocHolidays;
        }

        public AdHocHoliday AddAdHocHoliday(AdHocHoliday holiday) {
            _adhocHolidays.Add(holiday);
            return holiday;
        }

    }
}
