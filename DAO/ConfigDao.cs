using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PDFSigner.DAO
{
    class ConfigDao : IDao<Model.Config>
    {
        public Model.Config Get(int id)
        {
            Model.Config config = null;

            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "SELECT * FROM config WHERE id = @param_1 LIMIT 1",
                CommandType = System.Data.CommandType.Text
            };
            comm.Parameters.Add(new SQLiteParameter("@param_1", id));
            using var reader = comm.ExecuteReader();
            while(reader.Read())
            {
                config = Build(reader);
            }
            return config;
        }

        public void Delete(int id)
        {
            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "DELETE FROM config WHERE id = @param_1",
                CommandType = System.Data.CommandType.Text
            };
            comm.Parameters.Add(new SQLiteParameter("@param_1", id));
            comm.ExecuteNonQuery();
        }

        public Model.Config Inset(Model.Config model)
        {
            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "INSERT INTO config " +
                "(x, y, output_folder, overlap, signer_text, signer_visible, text_signer_visible, number_page, first_page, last_page, img, size_img, font_size, name) " +
                "VALUES (@param_1, @param_2, @param_3, @param_4, @param_5, @param_6, @param_7, @param_8, @param_9, @param_10, @param_11, @param_12, @param_13, @param_14)",
                CommandType = System.Data.CommandType.Text
            };
            comm.Parameters.Add(new SQLiteParameter("@param_1", model.X));
            comm.Parameters.Add(new SQLiteParameter("@param_2", model.Y));
            comm.Parameters.Add(new SQLiteParameter("@param_3", model.OutputFolder));
            comm.Parameters.Add(new SQLiteParameter("@param_4", model.Overlap));
            comm.Parameters.Add(new SQLiteParameter("@param_5", model.SignerText));
            comm.Parameters.Add(new SQLiteParameter("@param_6", model.SignerVisible));
            comm.Parameters.Add(new SQLiteParameter("@param_7", model.TextSignerVisible));
            comm.Parameters.Add(new SQLiteParameter("@param_8", model.NumberPage));
            comm.Parameters.Add(new SQLiteParameter("@param_9", model.FirstPage));
            comm.Parameters.Add(new SQLiteParameter("@param_10", model.LastPage));
            comm.Parameters.Add(new SQLiteParameter("@param_11", model.Img));
            comm.Parameters.Add(new SQLiteParameter("@param_12", model.SizeImg));
            comm.Parameters.Add(new SQLiteParameter("@param_13", model.FontSize));
            comm.Parameters.Add(new SQLiteParameter("@param_14", model.Name));
            comm.ExecuteNonQuery();

            model.ID = LastID();
            return model;
        }

        public List<Model.Config> ListAll()
        {
            List<Model.Config> config = new List<Model.Config>();

            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "SELECT * FROM config",
                CommandType = System.Data.CommandType.Text
            };
            using var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                config.Add(Build(reader));
            }
            return config;
        }

        public List<Model.Config> ListWithLimits(int limit)
        {
            List<Model.Config> config = new List<Model.Config>();

            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "SELECT * FROM config LIMIT @param_1",
                CommandType = System.Data.CommandType.Text
            };
            comm.Parameters.Add(new SQLiteParameter("@param_1", limit));
            using var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                config.Add(Build(reader));
            }
            return config;
        }

        public Model.Config Update(Model.Config model)
        {
            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "UPDATE config SET " +
                "x = @param_1, y = @param_2, output_folder = @param_3, overlap = @param_4, signer_text = @param_5, signer_visible = @param_6," +
                "text_signer_visible = @param_7, number_page = @param_8, first_page = @param_9, last_page = @param_10, img = @param_11, size_img = @param_12," +
                "font_size = @param_13, name = @param_14 WHERE id = @param_15",
                CommandType = System.Data.CommandType.Text
            };
            comm.Parameters.Add(new SQLiteParameter("@param_1", model.X));
            comm.Parameters.Add(new SQLiteParameter("@param_2", model.Y));
            comm.Parameters.Add(new SQLiteParameter("@param_3", model.OutputFolder));
            comm.Parameters.Add(new SQLiteParameter("@param_4", model.Overlap));
            comm.Parameters.Add(new SQLiteParameter("@param_5", model.SignerText));
            comm.Parameters.Add(new SQLiteParameter("@param_6", model.SignerVisible));
            comm.Parameters.Add(new SQLiteParameter("@param_7", model.TextSignerVisible));
            comm.Parameters.Add(new SQLiteParameter("@param_8", model.NumberPage));
            comm.Parameters.Add(new SQLiteParameter("@param_9", model.FirstPage));
            comm.Parameters.Add(new SQLiteParameter("@param_10", model.LastPage));
            comm.Parameters.Add(new SQLiteParameter("@param_11", model.Img));
            comm.Parameters.Add(new SQLiteParameter("@param_12", model.SizeImg));
            comm.Parameters.Add(new SQLiteParameter("@param_13", model.FontSize));
            comm.Parameters.Add(new SQLiteParameter("@param_14", model.Name));
            comm.Parameters.Add(new SQLiteParameter("@param_15", model.ID));
            comm.ExecuteNonQuery();
            return model;
        }

        public Model.Config Build(SQLiteDataReader reader)
        {
            Model.Config config = new Model.Config
            {
                ID = reader.GetInt32(0)
            };
            if (!reader.IsDBNull(1))  config.X = reader.GetFloat(1);
            if (!reader.IsDBNull(2))  config.Y = reader.GetFloat(2);
            if (!reader.IsDBNull(3))  config.OutputFolder = reader.GetString(3);
            if (!reader.IsDBNull(4))  config.Overlap = reader.GetBoolean(4);
            if (!reader.IsDBNull(5))  config.SignerText = reader.GetString(5);
            if (!reader.IsDBNull(6))  config.SignerVisible = reader.GetBoolean(6);
            if (!reader.IsDBNull(7))  config.TextSignerVisible = reader.GetBoolean(7);
            if (!reader.IsDBNull(8))  config.NumberPage = reader.GetInt32(8);
            if (!reader.IsDBNull(9))  config.FirstPage = reader.GetBoolean(9);
            if (!reader.IsDBNull(10))  config.LastPage = reader.GetBoolean(10);
            if (!reader.IsDBNull(11))  config.Img = reader.GetString(11);
            if (!reader.IsDBNull(12))  config.SizeImg = reader.GetFloat(12);
            if (!reader.IsDBNull(13))  config.FontSize = reader.GetFloat(13);
            if (!reader.IsDBNull(14)) config.Name = reader.GetString(14);
            return config;
        }

        public int LastID()
        {
            int id = 0;

            using var comm = new SQLiteCommand(ConnectionFactory.Connect())
            {
                CommandText = "SELECT MAX(id) FROM config",
                CommandType = System.Data.CommandType.Text
            };
            using var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            return id;
        }
    }
}
