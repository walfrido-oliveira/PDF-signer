using System;
using System.Configuration;

namespace PDFSigner.Config
{
    public class Config
    {
        public float GetX()
        {
            float.TryParse(ConfigurationManager.AppSettings.Get("x"), out float x);
            return x;
        }

        public float SetX(float value)
        {
            Write("x", value.ToString());
            return GetX();
        }

        public float GetY()
        {
            float.TryParse(ConfigurationManager.AppSettings.Get("y"), out float y);
            return y;
        }

        public float SetY(float value)
        {
            Write("y", value.ToString());
            return GetY();
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
            bool.TryParse(ConfigurationManager.AppSettings.Get("overlap"), out bool overlap);
            return overlap;
        }

        public bool SetOverlap(bool value)
        {
            Write("overlap", value.ToString());
            return GetOverlap();
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
            bool.TryParse(ConfigurationManager.AppSettings.Get("signerVisible"), out bool signerVisible);
            return signerVisible;
        }

        public bool SetSignerVisible(bool value)
        {
            Write("signerVisible", value.ToString());
            return GetSignerVisible();
        }

        public bool GetTextSignerVisible()
        {
            bool.TryParse(ConfigurationManager.AppSettings.Get("textSignerVisible"), out bool textSignerVisible);
            return textSignerVisible;
        }

        public bool SetTextSignerVisible(bool value)
        {
            Write("textSignerVisible", value.ToString());
            return GetTextSignerVisible();
        }

        public int GetNumberPage()
        {
            int.TryParse(ConfigurationManager.AppSettings.Get("numberPage"), out int numverPage);
            return numverPage;
        }

        public int SetNumberPage(int value)
        {
            Write("numberPage", value.ToString());
            return GetNumberPage();
        }

        public bool  GetFirstPage()
        {
            bool.TryParse(ConfigurationManager.AppSettings.Get("firstPage"), out bool firstPage);
            return firstPage;
        }

        public bool SetFirstPage(bool value)
        {
            Write("firstPage", value.ToString());
            return GetFirstPage();
        }

        public bool GetLastPage()
        {
            bool.TryParse(ConfigurationManager.AppSettings.Get("lastPage"), out bool lastPage);
            return lastPage;
        }

        public bool SetLastPage(bool value)
        {
            Write("lastPage", value.ToString());
            return GetLastPage();
        }

        public float GetSizeImg()
        {
            float.TryParse( ConfigurationManager.AppSettings.Get("sizeImg"), out float sizeImg);
            return sizeImg;
        }

        public float SetSizeImg(float value)
        {
            Write("sizeImg", value.ToString());
            return GetSizeImg();
        }

        public float GetFontSize()
        {
            float.TryParse(ConfigurationManager.AppSettings.Get("fontSize"), out float fontSize);
            return fontSize;
        }

        public float SetFontSize(float value)
        {
            Write("fontSize", value.ToString());
            return GetFontSize();
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
