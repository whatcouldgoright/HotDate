using System;
using System.ComponentModel.DataAnnotations;

namespace HotDate.Model
{
    public class AnnualHoliday : IHoliday {
        
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }
        [Required]
        [Range(1, 31)]
        public int Date { get; set; }
        public bool Rollover { get; set; } = false;

        public virtual DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(year, Month, Date);

            while(Rollover && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                date = date.AddDays(1);
                
            return date;
        }
    }
}
