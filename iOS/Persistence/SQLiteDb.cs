using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Tracker2.iOS;
using Tracker2.Persistence;

[assembly: Dependency(typeof(Tracker2.iOS.Persistence.SQLiteDb))]

namespace Tracker2.iOS.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            //File.Delete(path);
            return new SQLiteAsyncConnection(path);
        }
    }
}
