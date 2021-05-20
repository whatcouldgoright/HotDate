using System;
using System.ComponentModel.DataAnnotations;

namespace HotDate.Model
{
    // When Christmas day falls on a Saturday and Boxing Day falls on a Sunday,
    // Christmas day rolls on to the following Monday, and boxing day rolls to Tuesday.
    public class BoxingDayHoliday : AnnualHoliday {
        
        public override DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(year, Month, Date);
            DateTime effectiveDate = base.EffectiveDate(year);

            if(date.DayOfWeek == DayOfWeek.Sunday)
                effectiveDate = effectiveDate.AddDays(1);  // allow for Xmas public holiday.

            return effectiveDate;
        }
    }
}
