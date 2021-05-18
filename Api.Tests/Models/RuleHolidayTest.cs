using System;
using Xunit;
using HotDate.Model;

namespace Api.Services.Tests
{
    public class RuleHolidayTest
    {
        [Theory]

        [InlineData(2021, 6, 1, 1, 2, "2021-06-14" )]   // queens birthday, second Monday in June
        [InlineData(2021, 8, 1, 1, 1, "2021-08-02" )]   // bank holiday birthday, first Monday in August
        [InlineData(2021, 5, 1, 4, 1, "2021-05-06" )]   // world password day, first Thursday in May 
        public void GetEffectiveDate(int year, int month, int date, int dayOfWeek, int occurence, string expectedResultString)
        {
            DateTime expectedEffectiveDate = DateTime.Parse(expectedResultString);

            RuleHoliday holiday = new RuleHoliday {
                Name = "foo",
                Month = month,
                Date = date,
                DayOfWeek = dayOfWeek,
                Occurence = occurence
            };

            Assert.Equal(expectedEffectiveDate, holiday.EffectiveDate(year));
        }
    }
}
