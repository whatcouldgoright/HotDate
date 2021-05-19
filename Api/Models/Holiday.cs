using System;

namespace HotDate.Model
{
    public class Holiday : IHoliday {
        
        public string Name { get; set; }        
        public int Month { get; set; }
        public int Date { get; set; }
        public bool Rollover { get; set; }

        public virtual DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(year, Month, Date);

            while(Rollover && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                date = date.AddDays(1);
                
            return date;
        }
    }
}
