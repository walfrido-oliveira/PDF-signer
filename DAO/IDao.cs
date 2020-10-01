using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PDFSigner.DAO
{
    interface IDao<T>
    {
        T Get(int id);
        T Inset(T model);
        T Update(T model);
        void Delete(int id);
        List<T> ListAll();
        List<T> ListWithLimits(int limit);
        T Build(SQLiteDataReader reader);
        int LastID();
    }
}
