using System;
using Xunit;
using HotDate.Model;

namespace Api.Services.Tests
{
    public class HolidayTest
    {
        [Theory]
        [InlineData(2021, 01, 26, "2021-01-26")]   // anzac day
        // HS: TODO: Implement rollover test.
        public void GetEffectiveDate(int year, int month, int date, string expectedResultString)
        {
            DateTime expectedEffectiveDate = DateTime.Parse(expectedResultString);

            Holiday holiday = new Holiday {
                Name = "foo",
                Month = month,
                Date = date
            };

            Assert.Equal(expectedEffectiveDate, holiday.EffectiveDate(year));
        }
    }
}
