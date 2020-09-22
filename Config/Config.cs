using PDFSigner.Properties;
using System;
using System.Configuration;

namespace PDFSigner.Config
{
    public class Config
    {
        public float GetX()
        {
            return Settings.Default.x;
        }

        public float SetX(float value)
        {
            Settings.Default.x = value;
            Settings.Default.Save();
            return GetX();
        }

        public float GetY()
        {
            return Settings.Default.y;
        }

        public float SetY(float value)
        {
            Settings.Default.y = value;
            Settings.Default.Save();
            return GetY();
        }

        public string GetImg()
        {
            return Settings.Default.img;
        }

        public string SetImg(string value)
        {
            Settings.Default.img = value;
            Settings.Default.Save();
            return GetImg();
        }

        public string GetOutputFolder()
        {
            return Settings.Default.outputFolder;
        }

        public string SetOutputFolder(string value)
        {
            Settings.Default.outputFolder = value;
            Settings.Default.Save();
            return GetOutputFolder();
        }

        public bool GetOverlap()
        {
            return Settings.Default.overlap;
        }

        public bool SetOverlap(bool value)
        {
            Settings.Default.overlap = value;
            Settings.Default.Save();
            return GetOverlap();
        }

        public string GetSignerText()
        {
            return Settings.Default.signerText;
        }

        public string SetSignerText(string value)
        {
            Settings.Default.signerText = value;
            Settings.Default.Save();
            return GetSignerText();
        }

        public bool GetSignerVisible()
        {
            return Settings.Default.signerVisible;
        }

        public bool SetSignerVisible(bool value)
        {
            Settings.Default.signerVisible = value;
            Settings.Default.Save();
            return GetSignerVisible();
        }

        public bool GetTextSignerVisible()
        {
            return Settings.Default.textSignerVisible;
        }

        public bool SetTextSignerVisible(bool value)
        {
            Settings.Default.textSignerVisible = value;
            Settings.Default.Save();
            return GetTextSignerVisible();
        }

        public int GetNumberPage()
        {
            return Settings.Default.numberPage;
        }

        public int SetNumberPage(int value)
        {
            Settings.Default.numberPage = value;
            Settings.Default.Save();
            return GetNumberPage();
        }

        public bool  GetFirstPage()
        {
            return Settings.Default.firstPage;
        }

        public bool SetFirstPage(bool value)
        {
            Settings.Default.firstPage = value;
            Settings.Default.Save();
            return GetFirstPage();
        }

        public bool GetLastPage()
        {
            return Settings.Default.lastPage;
        }

        public bool SetLastPage(bool value)
        {
            Settings.Default.lastPage = value;
            Settings.Default.Save();
            return GetLastPage();
        }

        public float GetSizeImg()
        {
            return Settings.Default.sizeImg;
        }

        public float SetSizeImg(float value)
        {
            Settings.Default.sizeImg = value;
            Settings.Default.Save();
            return GetSizeImg();
        }

        public float GetFontSize()
        {
            return Settings.Default.fontSize;
        }

        public float SetFontSize(float value)
        {
            Settings.Default.fontSize = value;
            Settings.Default.Save();
            return GetFontSize();
        }
    }
}
