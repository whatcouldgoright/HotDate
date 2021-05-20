using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using Xunit;
using HotDate.Model;
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

            var calendarService = new CalendarService(new List<IHolidayService>());
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
        [InlineData("2021-01-11", "2021-01-27", 11)]    // multi weekend span
        [InlineData("2020-01-01", "2021-01-01", 261)]   // full year
        [InlineData("2015-01-01", "2021-01-01", 1565)]  // 5 years+
        [InlineData("2020-12-30", "2021-01-01", 1)]     // NYE shenanigans
        // HS TODO: Test public holiday in count by mocking
        public void GetBusinessDaysBetweenDates(string start, string end, int expectedResult)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            List<IHolidayService> services = new List<IHolidayService>();
            var calendarService = new CalendarService(services);

            var result = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            Assert.Equal(expectedResult, result);
            var fastResult = calendarService.FastBusinessDaysBetweenDates(startDate, endDate);
            Assert.Equal(expectedResult, fastResult);
        }

        [Theory]
        [InlineData("0100-01-01", "9999-12-31")]     // quite a while
        public void GetBusinessDaysPerformanceComparison(string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            List<IHolidayService> services = new List<IHolidayService>();
            var calendarService = new CalendarService(services);

            Stopwatch slowStopwatch = new Stopwatch();
            slowStopwatch.Start();
            var slowResult = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            slowStopwatch.Stop();
            var slowTime = slowStopwatch.ElapsedMilliseconds;

            Stopwatch fastStopwatch = new Stopwatch();
            fastStopwatch.Start();
            var fastResult = calendarService.FastBusinessDaysBetweenDates(startDate, endDate);
            fastStopwatch.Stop();
            var fastTime = fastStopwatch.ElapsedMilliseconds;

            Assert.True(fastTime < slowTime);
        }
        
    }
}
