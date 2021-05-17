namespace HotDate.Model
{
    public class PublicHoliday {
        public string Name { get; set;}
        public int Month { get; set; }
        public int Day { get; set; }
        public bool Additional { get; set;}
        public int OffsetDirection { get; set;}
        public int OffsetDayOfWeekCondition { get; set;}
        public int OffsetCount { get; set;}

        // public DateTime EffectiveDate(int year)
        // {

        // }
    }
}
