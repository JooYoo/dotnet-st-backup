using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace egConsole.Data.Models
{
    [Table("Console")]
    public class GameConsole
    {
        [Key]
        public int ConsoleID { get; set; }

        public string ConsoleName { get; set; }

        public string ConsoleCompany { get; set; }
    }
}
