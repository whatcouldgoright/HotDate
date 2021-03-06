using System;
using Xunit;
using HotDate.Model;

namespace Api.Services.Tests
{
    public class AdHocHolidayTest
    {
        [Theory]
        [InlineData(2021, 09, 25, "2021-09-25")]   // afl grand final 2021
        public void GetEffectiveDate(int year, int month, int date, string expectedResultString)
        {
            DateTime expectedEffectiveDate = DateTime.Parse(expectedResultString);

            AdHocHoliday holiday = new AdHocHoliday {
                Name = "foo",
                Year = year,
                Month = month,
                Date = date
            };

            Assert.Equal(expectedEffectiveDate, holiday.EffectiveDate(year));
        }
    }
}
