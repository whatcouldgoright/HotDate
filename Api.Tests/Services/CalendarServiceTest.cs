using System;
using Xunit;
using HotDate.Services;

namespace Api.Services.Tests
{
    public class CalendarServiceTest
    {
        [Fact]
        public void ThrowsErrorIfStartDateAfterEndDate()
        {
            DateTime fromDate = DateTime.Parse("2021-01-02");
            DateTime toDate = DateTime.Parse("2021-01-01");

            var calendarService = new CalendarService();
            try {
                calendarService.GetBusinessDaysBetweenDates(fromDate, toDate);
            }
            catch(Exception) {
                return;
            }
            Assert.True(false, "expected argument exception");
        }

        [Theory]

        [InlineData("2021-01-11", "2021-01-13", 1)]     // simple midweek count
        [InlineData("2021-01-11", "2021-01-11", 0)]     // same day should be zero
        [InlineData("2021-01-11", "2021-01-12", 0)]     // exclude start and end date
         
        [InlineData("2021-01-11", "2021-01-20", 6)]     // single weekend span
        [InlineData("2021-01-11", "2021-01-27", 10)]    // multi weekend span, including Aus Day public holiday
        public void GetBusinessDaysBetweenDates(string start, string end, int expectedResult)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            var calendarService = new CalendarService();

            var result = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            Assert.Equal(expectedResult, result);
        }
    }
}
