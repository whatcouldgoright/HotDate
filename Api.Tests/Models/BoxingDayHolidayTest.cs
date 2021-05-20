using System;
using Xunit;
using HotDate.Model;

namespace Api.Services.Tests
{
    public class BoxingDayHolidayTest
    {
        [Theory]
        [InlineData(2019, 12, 26, true, "2019-12-26")]   // Boxing Day 2019: Just Thursday
        [InlineData(2020, 12, 26, true, "2020-12-28")]   // Boxing Day 2020: Single Rollover...
        [InlineData(2021, 12, 26, true, "2021-12-28")]   // Boxing Day 2021: Double Rollover!!!
        public void GetEffectiveDate(int year, int month, int date, bool rollover, string expectedResultString)
        {
            DateTime expectedEffectiveDate = DateTime.Parse(expectedResultString);

            BoxingDayHoliday holiday = new BoxingDayHoliday {
                Name = "foo",
                Month = month,
                Date = date,
                Rollover = rollover
            };

            Assert.Equal(expectedEffectiveDate, holiday.EffectiveDate(year));
        }
    }
}
