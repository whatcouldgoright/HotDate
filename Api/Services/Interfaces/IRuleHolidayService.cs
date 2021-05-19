using System;
using System.Collections.Generic;
using HotDate.Model;

namespace HotDate.Services
{
    public interface IRuleHolidayService
    {
        RuleHoliday SaveHoliday(RuleHoliday holiday);
    }
}
