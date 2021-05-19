using System;
using System.Collections.Generic;
using System.Linq;
using HotDate.Model;
using HotDate.Storage;


namespace HotDate.Services
{
    public class AdHocHolidayService : IHolidayService, IAdhocHolidayService
    {
        private readonly IStorage _storage;

        public AdHocHolidayService(IStorage storage) {
            _storage = storage;
        }

        public bool IsHoliday(DateTime date)
        {
            return _storage.GetAdHocHolidays().Any(h => h.EffectiveDate(date.Year).Equals(date));
        }

        public IEnumerable<IHoliday> GetHolidays(int year) {
            return _storage.GetAdHocHolidays().Where(h => h.Year == year);
        }

        public IEnumerable<AdHocHoliday> GetHolidays() {

            return _storage.GetAdHocHolidays();
        }

        public AdHocHoliday SaveHoliday(AdHocHoliday holiday)
        {
            _storage.AddAdHocHoliday(holiday);
            return holiday;
        }

    }
}
