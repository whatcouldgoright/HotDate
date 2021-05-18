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
    public class HolidaysController : ControllerBase
    {
        private readonly ILogger<BusinessDaysController> _logger;
        private readonly IHolidayService _holidaysService;
        private readonly ICalendarService _calendarService;

        public HolidaysController(ILogger<BusinessDaysController> logger,
                                  ICalendarService calendarService)
        {
            _logger = logger;
            _calendarService = calendarService;
        }

        [HttpGet]
        public List<DateTime> Get([FromQuery] int year)
        {
            return _calendarService.GetHolidayDates(year);
        }

    }
}
 