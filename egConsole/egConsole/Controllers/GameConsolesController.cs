using egConsole.Data.Services;
using egConsole.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace egConsole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameConsolesController : ControllerBase
    {
        // inject service
        public GameConsoleService _gameConsoleService;

        public GameConsolesController(GameConsoleService gameConsoleService)
        {
            _gameConsoleService = gameConsoleService;
        }

        [HttpGet("get-all-gameconsoles")]
        public IActionResult GetAllGameConsoles()
        {
            var allGameConsoles = _gameConsoleService.GetAllGameConsoles();
            return Ok(allGameConsoles);
        }

        [HttpGet("get-gameconsole-by-id/{id}")]
        public IActionResult GetGameConsoleById(int id)
        {
            var theGameConsole = _gameConsoleService.GetGameConsoleById(id);
            return Ok(theGameConsole);
        }

        [HttpGet("get-consolecompany-by-consolename/{consoleName}")]
        public IActionResult GetCompanyByConsoleName(string consoleName)
        {
            var theGameCompany = _gameConsoleService.GetCompanyByName(consoleName);
            return Ok(theGameCompany);
        }

        [HttpPost("add-gameconsole")]
        public IActionResult AddGameConsole([FromBody]GameConsoleVM gameConsole)
        {
            _gameConsoleService.AddGameConsole(gameConsole);
            return Ok();
        }

        [HttpPut("put-gameconsole-by-id/{id}")]
        public IActionResult UpdateGameConsoleById(int id, [FromBody]GameConsoleVM gameConsole)
        {
            var theGameConsole = _gameConsoleService.UpdateGameConsoleById(id, gameConsole);
            return Ok(theGameConsole);
        }

        [HttpDelete("delete-gameconsole-by-id/{id}")]
        public IActionResult DeleteGameConsoleById(int id)
        {
            _gameConsoleService.DeleteGameConsoleById(id);
            return Ok();
        }

        // TEMP: ✅ return List<T>
        [HttpGet("sp-get-all")]
        public IActionResult SqGetAll()
        {
            var theAll = _gameConsoleService.GetAllSp();
            return Ok(theAll);
        }

        // TEMP: ✅ @Parameter, return List<T>
        [HttpGet("sp-get-consoles-by-company/{company}")]
        public IActionResult SqGetConsolesByCompany(string company)
        {
            var theConsoles = _gameConsoleService.GetConsolesByCompanySp(company);
            return Ok(theConsoles);
        }

        // FIXME: ❎ @Parameter, return string
        [HttpGet("sp-get-company-by-console/{consoleName}")]
        public IActionResult SqGetCompanyByConsoleName(string consoleName)
        {
            var theGameCompany = _gameConsoleService.GetCompanyByConsoleSp(consoleName);
            return Ok(theGameCompany);
        }
    }
}
