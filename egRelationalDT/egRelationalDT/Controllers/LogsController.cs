using egRelationalDT.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace egRelationalDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;
        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs")]
        public IActionResult GetAllLogs()
        {
            try
            {
                var logs = _logsService.GetAllLogsFromDb();
                return Ok(logs);
            }
            catch (Exception)
            {
                return BadRequest("Could not load logs from DB.");
            }
        }
    }
}
