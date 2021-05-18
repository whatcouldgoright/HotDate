using System;

namespace HotDate.Model
{
    public class Holiday : IHoliday {
        
        public string Name { get; set; }        
        public int Month { get; set; }
        public int Date { get; set; }
        public virtual DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(year, Month, Date);
            return date;
        }
    }
}
