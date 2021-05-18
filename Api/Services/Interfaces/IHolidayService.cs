using System;
using System.Collections.Generic;
using HotDate.Model;

namespace HotDate.Services
{
    public interface IHolidayService
    {
        IEnumerable<IHoliday> GetHolidays(int year);
        bool IsHoliday(DateTime date);
    }
}
