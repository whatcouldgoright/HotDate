using System;
using System.ComponentModel.DataAnnotations;

namespace HotDate.Model
{
    public class BusinessDaysQuery {

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FromDate { get; set;}
        
        [Required]
        [DataType(DataType.DateTime)]     
        public DateTime ToDate { get; set;}
        
    }

}
