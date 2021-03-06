using egConsole.Data.Models;
using egConsole.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace egConsole.Data.Services
{
    public class GameConsoleService
    {
        private AppDbContext _context { get; set; }

        public GameConsoleService(AppDbContext context)
        {
            _context = context;
        }

        public void AddGameConsole(GameConsoleVM gameConsole)
        {
            var _gameConsole = new GameConsole()
            {
                ConsoleName = gameConsole.ConsoleName,
                ConsoleCompany = gameConsole.ConsoleCompany
            };

            _context.GameConsoles.Add(_gameConsole);
            _context.SaveChanges();
        }

        public List<GameConsole> GetAllGameConsoles()
        {
            return _context.GameConsoles.ToList();
        }

        public GameConsole GetGameConsoleById(int gameConsoleId)
        {
            return _context.GameConsoles.FirstOrDefault(g => g.ConsoleID == gameConsoleId);
        }

        public string GetCompanyByName(string consoleName)
        {
            var gameConsole = _context.GameConsoles.FirstOrDefault(g => g.ConsoleName == consoleName);
            return gameConsole.ConsoleCompany;
        }

        public GameConsole UpdateGameConsoleById(int id, GameConsoleVM gameConsole)
        {
            // find object in DB by Id
            var _gameConsole = _context.GameConsoles.FirstOrDefault(g => g.ConsoleID == id);

            // update object via VM and change object properties in DB
            if (_gameConsole != null)
            {
                _gameConsole.ConsoleName = gameConsole.ConsoleName;
                _gameConsole.ConsoleCompany = gameConsole.ConsoleCompany;

                _context.SaveChanges();
            }

            return _gameConsole;
        }

        public void DeleteGameConsoleById(int id)
        {
            // find object in DB
            var _gameConsole = _context.GameConsoles.FirstOrDefault(g => g.ConsoleID == id);

            // delete the object in DB
            if (_gameConsole != null)
            {
                _context.GameConsoles.Remove(_gameConsole);
                _context.SaveChanges();
            }
        }

        // TEMP: ✅ return List<T>
        public List<GameConsole> GetAllSp()
        {
            return _context.GameConsoles.FromSqlRaw("EXECUTE dbo.GetAll").ToList();
        }

        // TEMP: ✅ @Parameter, return List<T>
        public List<GameConsole> GetConsolesByCompanySp(string company)
        {
            return _context.GameConsoles.FromSqlRaw("EXECUTE dbo.GetConsolesByCompany {0}", company).ToList();
        }

        // FIXME: ❎ @Parameter, return string
        public string GetCompanyByConsoleSp(string consoleName)
        {
            var test = _context.GameConsoles.FromSqlRaw("EXECUTE dbo.GetCompanyByName {0}", consoleName);
            return _context.GameConsoles.FromSqlRaw("EXECUTE dbo.GetCompanyByName", consoleName).ToString();
        }
    }
}
