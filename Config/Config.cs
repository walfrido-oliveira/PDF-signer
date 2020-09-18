using System;
using System.Configuration;

namespace PDFSigner.Config
{
    public class Config
    {
        public float GetX()
        {
            return float.Parse(ConfigurationManager.AppSettings.Get("x"));
        }

        public float SetX(double value)
        {
            Write("x", value.ToString());
            return float.Parse(ConfigurationManager.AppSettings.Get("x"));
        }

        public float GetY()
        {
            return float.Parse(ConfigurationManager.AppSettings.Get("y"));
        }

        public float SetY(double value)
        {
            Write("y", value.ToString());
            return float.Parse(ConfigurationManager.AppSettings.Get("y"));
        }

        public string GetImg()
        {
            return ConfigurationManager.AppSettings.Get("img");
        }

        public string SetImg(string value)
        {
            Write("img", value);
            return ConfigurationManager.AppSettings.Get("img");
        }

        public string GetOutputFolder()
        {
            return ConfigurationManager.AppSettings.Get("outputFolder");
        }

        public string SetOutputFolder(string value)
        {
            Write("outputFolder", value);
            return ConfigurationManager.AppSettings.Get("outputFolder");
        }

        public bool GetOverlap()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get("overlap"));
        }

        public bool SetOverlap(bool value)
        {
            Write("overlap", value.ToString());
            return bool.Parse(ConfigurationManager.AppSettings.Get("overlap"));
        }

        public string GetSignerText()
        {
            return ConfigurationManager.AppSettings.Get("signerText");
        }

        public string SetSignerText(string value)
        {
            Write("signerText", value);
            return ConfigurationManager.AppSettings.Get("signerText");
        }

        public bool GetSignerVisible()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get("signerVisible"));
        }

        public bool SetSignerVisible(bool value)
        {
            Write("signerVisible", value.ToString());
            return bool.Parse(ConfigurationManager.AppSettings.Get("signerVisible"));
        }

        public bool GetTextSignerVisible()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get("textSignerVisible"));
        }

        public bool SetTextSignerVisible(bool value)
        {
            Write("textSignerVisible", value.ToString());
            return bool.Parse(ConfigurationManager.AppSettings.Get("textSignerVisible"));
        }

        public int GetNumberPage()
        {
            return int.Parse(ConfigurationManager.AppSettings.Get("numberPage"));
        }

        public int SetNumberPage(int value)
        {
            Write("numberPage", value.ToString());
            return int.Parse(ConfigurationManager.AppSettings.Get("numberPage"));
        }

        public bool  GetFirstPage()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get("firstPage"));
        }

        public bool SetFirstPage(bool value)
        {
            Write("firstPage", value.ToString());
            return bool.Parse(ConfigurationManager.AppSettings.Get("firstPage"));
        }

        public bool GetLastPage()
        {
            return bool.Parse(ConfigurationManager.AppSettings.Get("lastPage"));
        }

        public bool SetLastPage(bool value)
        {
            Write("lastPage", value.ToString());
            return bool.Parse(ConfigurationManager.AppSettings.Get("lastPage"));
        }

        public float GetSizeImg()
        {
            return float.Parse( ConfigurationManager.AppSettings.Get("sizeImg"));
        }

        public float SetSizeImg(float value)
        {
            Write("sizeImg", value.ToString());
            return float.Parse(ConfigurationManager.AppSettings.Get("sizeImg"));
        }

        public float GetFontSize()
        {
            return float.Parse(ConfigurationManager.AppSettings.Get("fontSize"));
        }

        public float SetFontSize(float value)
        {
            Write("fontSize", value.ToString());
            return float.Parse(ConfigurationManager.AppSettings.Get("fontSize"));
        }

        public void Write(string key, string value)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (conf.AppSettings.Settings[key] == null)
                {
                    conf.AppSettings.Settings.Add(key, value);
                }
                else
                {
                    conf.AppSettings.Settings[key].Value = value;
                }
                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(conf.AppSettings.SectionInformation.Name);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
