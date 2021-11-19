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
    }
}
