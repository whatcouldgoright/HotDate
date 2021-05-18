using System;

namespace HotDate.Model
{
    public interface IHoliday {

        string Name { get; set; }        
        int Month { get; set; }
        int Date { get; set; }

        DateTime EffectiveDate(int year);
    }
}
