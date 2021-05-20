using System;

namespace HotDate.Model
{
    public interface IHoliday {

        string Name { get; set; }        
        int Month { get; set; }
        int Date { get; set; }

        /// <summary>Method <c>EffectiveDate</c> returns the DateTime upon which a holiday will occur for a given year.  
        /// This may be null for some types of holidays!</summary>
        DateTime EffectiveDate(int year);
    }
}
