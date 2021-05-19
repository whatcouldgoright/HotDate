using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotDate.Model;
using HotDate.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly ILogger<BusinessDaysController> _logger;
        private readonly IAdhocHolidayService _adhocHolidayService;

        public HolidaysController(ILogger<BusinessDaysController> logger,
                                  IAdhocHolidayService adhocHolidayService)
        {
            _logger = logger;
            _adhocHolidayService = adhocHolidayService;
        }

        [HttpGet]
        [Route("Adhoc")]
        public IEnumerable<AdHocHoliday> Get([FromQuery] int year)
        {
            return _adhocHolidayService.GetHolidays();
        }

        [HttpPost]
        [Route("Adhoc")]
        public AdHocHoliday Post([FromBody] AdHocHoliday holiday)
        {
            return _adhocHolidayService.SaveHoliday(holiday);
        }

    }
}
 