using egRelationalDT.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace egRelationalDT.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDb()
        {
            return _context.Logs.ToList();
        }
    }
}
