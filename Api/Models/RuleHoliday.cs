using System;
using System.ComponentModel.DataAnnotations;

namespace HotDate.Model
{
    /// <summary>Class <c>RuleHoliday</c> represents holidays defined as occurence N of a day after a time,
    /// for example, 'Second Monday in August'.</summary>
    public class RuleHoliday : AnnualHoliday {

        [Required]
        [Range(0,6)]
        public int DayOfWeek { get; set;}
        [Required]
        [Range(1, 4)]
        public int Occurence { get; set;}

        public override DateTime EffectiveDate(int year)
        {
            DateTime effectiveDate = new DateTime(year, Month, Date);
            var occurenceCount = 0;
            while(occurenceCount < Occurence)
            {
                if((int)effectiveDate.DayOfWeek == DayOfWeek)
                    occurenceCount++;
                
                if(occurenceCount < Occurence)
                    effectiveDate = effectiveDate.AddDays(1);
            }
            return effectiveDate;
        }
    }

}
