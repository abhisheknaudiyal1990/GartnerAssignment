using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gartner.DataContracts
{
    public class DBService : IDataService
    {
        private readonly ILogger<DBService> _logger;
        public DBService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DBService>();
        }

        public bool ParseData()
        {
            return true;//MySQL to MongoDB
        }
    }
}
