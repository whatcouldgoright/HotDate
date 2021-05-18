using System;

namespace HotDate.Model
{
    public class FixedHoliday : Holiday {
        
        public int Year { get; set; }

        public override DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(Year, Month, Date);
            return date;
        }
    }

}
