using System;
using System.Collections.Generic;

namespace PDFSigner.Model
{
    public class Config
    {
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string OutputFolder { get; set; }
        public bool Overlap { get; set; }
        public string SignerText {get; set; }
        public bool SignerVisible { get; set; }
        public bool TextSignerVisible { get; set; }
        public int NumberPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public string Img { get; set; }
        public float SizeImg { get; set; }
        public float FontSize { get; set; }
        public string Name { get; set; }

        public Config Get(int id)
        {
            return new DAO.ConfigDao().Get(id);
        }

        public void Delete()
        {
            new DAO.ConfigDao().Delete(ID);
        }

        public Config Insert()
        {
            return new DAO.ConfigDao().Inset(this);
        }

        public Config Update()
        {
            return new DAO.ConfigDao().Update(this);
        }

        public List<Config> ListAll()
        {
            return new DAO.ConfigDao().ListAll();
        }

        public List<Config> ListWithLimits(int limit)
        {
            return new DAO.ConfigDao().ListWithLimits(limit);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            Config config = (Config)obj;
            return ID.Equals(config.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public void InsertOrUpdate()
        {
            if (ID == 0)
                Insert();
            else
                Update();
        }
    }
}
