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
    public class PublicHolidaysController : ControllerBase
    {
        private readonly ILogger<BusinessDaysController> _logger;
        private readonly IHolidayService _holidaysService;

        public PublicHolidaysController(ILogger<BusinessDaysController> logger,
                                       IHolidayService holidaysService)
        {
            _logger = logger;
            _holidaysService = holidaysService;
        }

        [HttpGet]
        public IEnumerable<PublicHoliday> GetHolidays()
        {
            return _holidaysService.GetPublicHolidays();
        }
    }
}
 