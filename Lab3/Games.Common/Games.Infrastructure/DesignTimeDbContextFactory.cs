using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GamesContext>
    {
        public GamesContext CreateDbContext(string[] args)
        {
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "..", "data");
            Directory.CreateDirectory(dataDir);
            var conn = $"Data Source={Path.Combine(dataDir, "games.db")}";

            var optionsBuilder = new DbContextOptionsBuilder<GamesContext>();
            optionsBuilder.UseSqlite(conn);
            return new GamesContext(optionsBuilder.Options);
        }
    }
}