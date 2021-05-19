using System;
using Xunit;
using HotDate.Model;

namespace Api.Services.Tests
{
    public class AnnualHolidayTest
    {
        [Theory]
        [InlineData(2021, 01, 26, false, "2021-01-26")]   // anzac day
        [InlineData(2021, 12, 25, true, "2021-12-27")]   // Xmas day
        //[InlineData(2021, 01, 26, true, "2021-01-26")]   // HS: TODO:  Boxing Day: Dobule Rollover!
        public void GetEffectiveDate(int year, int month, int date, bool rollover, string expectedResultString)
        {
            DateTime expectedEffectiveDate = DateTime.Parse(expectedResultString);

            AnnualHoliday holiday = new AnnualHoliday {
                Name = "foo",
                Month = month,
                Date = date,
                Rollover = rollover
            };

            Assert.Equal(expectedEffectiveDate, holiday.EffectiveDate(year));
        }
    }
}
