using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotDate.Model;
using HotDate.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessDaysController : ControllerBase
    {
        private readonly ILogger<BusinessDaysController> _logger;
        private readonly ICalendarService _calendarService;

        public BusinessDaysController(ILogger<BusinessDaysController> logger,
                                      ICalendarService calendarService)
        {
            _logger = logger;
            _calendarService = calendarService;
        }

        [HttpGet]
        
        public string Get([FromQuery] BusinessDaysQuery businessDaysQuery)
        {
            DateTime fromDateTime = businessDaysQuery.FromDate;
            DateTime toDateTime = businessDaysQuery.ToDate;

            var result = _calendarService.GetBusinessDaysBetweenDates(fromDateTime, toDateTime);
            
            return $"There are {result} business days between {fromDateTime.ToShortDateString()} and {toDateTime.ToShortDateString()}";
        }
    }
}
 