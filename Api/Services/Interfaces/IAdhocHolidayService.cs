using System;
using System.Collections.Generic;
using HotDate.Model;

namespace HotDate.Services
{
    public interface IAdhocHolidayService
    {
        IEnumerable<AdHocHoliday> GetHolidays();
        AdHocHoliday SaveHoliday(AdHocHoliday holiday);
    }
}
