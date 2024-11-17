using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace CreativProp.Repositories
{
	public class Database
	{
        public static SQLiteAsyncConnection Connection;

        public Database(string dbPath)
		{
            Connection = new SQLiteAsyncConnection(dbPath);
        }
    }
}

