using System;
using Xunit;
using Hotdate.Services.Rules.Holidays;

namespace Services.Tests
{
    public class WeekendRuleTest
    {
        [Theory]

        [InlineData("2021-01-01", false)]   // Friday
        [InlineData("2021-01-02", true)]    // Saturday
        [InlineData("2021-01-03", true)]    // Sunday
        [InlineData("2021-01-04", false)]   // Monday
        [InlineData("2021-01-05", false)]   // Tuesday
        [InlineData("2021-01-06", false)]   // Wednesday
        [InlineData("2021-01-07", false)]   // Thursday
        public void WeekendRuleApplies(string date, bool expectedResult)
        {
            DateTime testDate = DateTime.Parse(date);
            var weekendRule = new IsWeekendRule();
            Assert.Equal(expectedResult, weekendRule.IsHoliday(testDate));
        }
    }
}
