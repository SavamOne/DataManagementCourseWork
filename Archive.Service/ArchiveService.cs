using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Archive.Service.Contracts;

namespace Archive.Service
{
    public class ArchiveService
    {
        private DbContext context;
        public DatabaseProcessor DatabaseProcessor { get; }

        public ArchiveService(DbContext context)
        {
            this.context = context;
            DatabaseProcessor = new DatabaseProcessor(context);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        public static bool TryCreateService(AuthData data, out ArchiveService service)
        {
            try
            {
                var dbContext = new DbContext(data.Host, data.Port, data.Login, data.Password);
                service = new ArchiveService(dbContext);
                return true;
            }
            catch
            {
                service = null;
                return false;
            }
            
        }
    }
}
