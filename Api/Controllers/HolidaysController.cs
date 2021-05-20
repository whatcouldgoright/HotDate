using System;
using System.Collections.Generic;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<AdHocHoliday> Get([FromQuery] int year)
        {
            return _adhocHolidayService.GetHolidays();
        }

        [HttpPost]
        [Route("Adhoc")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public AdHocHoliday Post([FromBody] AdHocHoliday holiday)
        {
            return _adhocHolidayService.SaveHoliday(holiday);
        }

    }
}
 