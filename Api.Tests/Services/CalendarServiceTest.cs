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
        [InlineData("2010-01-01", "2021-01-01", 2869)]  // 10 years+
        [InlineData("2020-12-30", "2021-01-01", 1)]     // NYE shenanigans
        public void GetBusinessDaysBetweenDatesAccountsForWeekends(string start, string end, int expectedResult)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            List<IHolidayService> services = new List<IHolidayService>();
            var calendarService = new CalendarService(services);

            var result = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            var fastResult = calendarService.FastBusinessDaysBetweenDates(startDate, endDate);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, fastResult);
        }

        [Theory]
        [InlineData("2021-03-01", "2021-03-05", "", 3)]             // without holiday
        [InlineData("2021-03-01", "2021-03-05", "2021-03-03", 2)]   // with holiday
        [InlineData("2021-03-03", "2021-03-10", "2021-03-05", 3)]   // handles weekends and holidays
        [InlineData("2021-03-03", "2021-03-10", "2021-03-06", 4)]   // doesn't double count holidays if they fall on a weekend
        public void GetBusinessDaysBetweenDatesAccountsForHolidays(string start, string end, string holidayStr, int expectedResult)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            
            List<IHolidayService> services = new List<IHolidayService>();
            List<IHoliday> holidays = new List<IHoliday>();

            if(!String.IsNullOrWhiteSpace(holidayStr))
            {
                DateTime holidayDate = DateTime.Parse(holidayStr);
                if(!String.IsNullOrWhiteSpace(holidayStr)) {
                    AdHocHoliday holiday = new AdHocHoliday {
                        Name = "Foo",
                        Year = holidayDate.Year,
                        Month = holidayDate.Month,
                        Date = holidayDate.Day            
                    };
                    holidays.Add(holiday);
                }
            }
            
            var holidayServiceMock = new Mock<IHolidayService>();
            holidayServiceMock.Setup(s => s.GetHolidays(It.IsAny<int>())).Returns(holidays);
            services.Add(holidayServiceMock.Object);
            var calendarService = new CalendarService(services);

            var result = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            var fastResult = calendarService.FastBusinessDaysBetweenDates(startDate, endDate);

            Assert.Equal(expectedResult, result);
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

        /*
         * Catch-all test of last resort to detect edge cases across large and complex calculations by comparing normal and fast calcs
         */
        [Fact]
        public void LongRangeConsistencyCheck()
        {
            DateTime startDate = DateTime.Parse("0001-01-01");
            DateTime endDate = DateTime.Parse("9999-12-31");
            
            List<IHolidayService> services = new List<IHolidayService>();
            List<IHoliday> holidays = new List<IHoliday>();

            holidays.Add(new AnnualHoliday { Name = "Annual", Month = 1, Date = 26 });
            holidays.Add(new AnnualHoliday { Name = "Rollover Annual", Month = 12, Date = 25 });

            holidays.Add(new RuleHoliday { Name = "Rule", Month = 6, Date = 1, DayOfWeek = (int) DayOfWeek.Monday, Occurence = 2 });

            holidays.Add(new AdHocHoliday { Name = "Weekend Adhoc", Year = 2021, Month = 7, Date = 2 });
            holidays.Add(new AdHocHoliday { Name = "Weekday Adhoc", Year = 2021, Month = 7, Date = 3 });

            var holidayServiceMock = new Mock<IHolidayService>();
            holidayServiceMock.Setup(s => s.GetHolidays(It.IsAny<int>())).Returns(holidays);
            services.Add(holidayServiceMock.Object);

            var calendarService = new CalendarService(services);
            var result = calendarService.GetBusinessDaysBetweenDates(startDate, endDate);
            var fastResult = calendarService.FastBusinessDaysBetweenDates(startDate, endDate);

            Assert.Equal(fastResult, result);
        }
        
    }
}
