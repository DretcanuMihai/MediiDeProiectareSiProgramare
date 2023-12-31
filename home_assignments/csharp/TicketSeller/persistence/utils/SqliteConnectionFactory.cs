﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace persistence.utils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            String connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}