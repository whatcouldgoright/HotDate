using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotDate.Model;
using HotDate.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FastBusinessDaysController : ControllerBase
    {
        private readonly ILogger<BusinessDaysController> _logger;
        private readonly ICalendarService _calendarService;

        public FastBusinessDaysController(ILogger<BusinessDaysController> logger,
                                      ICalendarService calendarService)
        {
            _logger = logger;
            _calendarService = calendarService;
        }

        [HttpGet]
        
        public BusinessDaysResponse Get([FromQuery] BusinessDaysQuery businessDaysQuery)
        {
            DateTime fromDateTime = businessDaysQuery.FromDate;
            DateTime toDateTime = businessDaysQuery.ToDate;

            var result = _calendarService.FastBusinessDaysBetweenDates(fromDateTime, toDateTime);
            
            return new BusinessDaysResponse {
                Count = result
            };
        }
    }
}
 