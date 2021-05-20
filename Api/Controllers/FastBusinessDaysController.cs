using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HotDate.Model;
using HotDate.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
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

        /// <summary>
        /// Returns a count of business days between two provided dates, exclusive.
        /// </summary>
        /// <param name="businessDaysQuery"></param> 
        /// <returns>A newly created BusinessDaysResponse with count populated</returns>
        /// <response code="201">Returns the count</response>
        /// <response code="400">If arguments are invalid</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
 