using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsTools
{
    public class DbAccess
    {
        public readonly DBContext DbContext;

        public DbAccess()
        {
            // TODO use secrets
            var conString = "Server=localhost,1433\\Catalog=sql1;Database=sql1;User=SA;Password=<YourStrong@Passw0rd>;";
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(conString);
            DbContext = new DBContext(optionsBuilder.Options);
        }
    }
}