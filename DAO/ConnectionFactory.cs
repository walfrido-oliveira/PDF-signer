using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSigner.DAO
{
    class ConnectionFactory
    {
        public static System.Data.SQLite.SQLiteConnection Connect()
        {
            var conn = new System.Data.SQLite.SQLiteConnection("Data Source= signerpdf.db");
            conn.Open();
            return conn;
        }
    }
}
