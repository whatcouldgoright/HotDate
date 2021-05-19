using System;
using System.ComponentModel.DataAnnotations;

namespace HotDate.Model
{
    public class AdHocHoliday : IHoliday {
        
        [Required]
        public string Name { get; set; }      
        [Required]  
        [Range(1, Int32.MaxValue)]
        public int Year { get; set; }
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }
        [Required]
        [Range(1, 31)]
        public int Date { get; set; }

        public DateTime EffectiveDate(int year)
        {
            DateTime date = new DateTime(Year, Month, Date);
            return date;
        }
    }

}
