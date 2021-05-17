using System;
using Xunit;
using Hotdate.Services.Rules.Holidays;

namespace Services.Tests
{
    public class publicHolidayRuleTest
    {
        [Theory]

        [InlineData("2021-01-25", false)]   // Normal Monday
        [InlineData("2021-01-26", true)]    // Aus Day
        public void PublicHolidayRuleApplies(string date, bool expectedResult)
        {
            DateTime testDate = DateTime.Parse(date);
            var publicHolidayRule = new IsPublicHolidayRule();
            Assert.Equal(expectedResult, publicHolidayRule.IsHoliday(testDate));
        }
    }
}
