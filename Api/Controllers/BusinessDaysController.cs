using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public string Get([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            DateTime fromDateTime = DateTime.Parse(fromDate);
            DateTime toDateTime = DateTime.Parse(toDate);

            var result = _calendarService.GetBusinessDaysBetweenDates(fromDateTime, toDateTime);
            
            return $"There are {result} business days between {fromDate} and {toDate}";
        }
    }
}
 