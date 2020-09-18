using iTextSharp.text.pdf;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text.pdf.security;
using iTextSharp.text;
using System;
using Org.BouncyCastle.X509;

namespace PDFSigner.Signer
{
    class Signer
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public Config.Config Config { get; set; }

        public Signer(string input, string output, Config.Config config)
        {
            Input = input;
            Output = output;
            Config = config;
            ConfigOutput();
        }

        public bool Signin()
        {
            try
            {
                X509Certificate2 cert = ExternalSignature.GetExternalSignature();
                if (cert == null) return false;

                X509CertificateParser cp = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
                IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");

                PdfReader reader = new PdfReader(Input);
                PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(Output, FileMode.Create, FileAccess.Write), '\0', null, true);
                PdfSignatureAppearance sap = st.SignatureAppearance;
                ConfigPDFSignatureAppearance(sap, reader);

                MakeSignature.SignDetached(sap, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

                if (Config.GetOverlap())
                {
                    File.Delete(Input);
                    File.Copy(Output, Input);
                    File.Delete(Output);
                }

                st.Close();
                return true;

            } catch (Exception)
            {
                return false;
            }
        }

        private void ConfigPDFSignatureAppearance(PdfSignatureAppearance sap, PdfReader reader)
        {
            int numberPage = 0;

            if (Config.GetFirstPage()) numberPage = 1;
            if (Config.GetLastPage()) numberPage = reader.NumberOfPages;
            if (!Config.GetLastPage() && !Config.GetFirstPage()) numberPage = Config.GetNumberPage();
            
            if (Config.GetSignerVisible())
            {
                sap.Layer2Text = "";
                if (Config.GetTextSignerVisible())
                {
                    sap.Layer2Text = Config.GetSignerText();
                    sap.Layer2Font = new Font(Font.FontFamily.TIMES_ROMAN, Config.GetFontSize());
                }

                if (!File.Exists(Config.GetImg())) throw new Exception(string.Format("Image for signer not found.", Config.GetImg()));
                sap.Image = Image.GetInstance(Config.GetImg());

                float ury = ((Config.GetX() / 100) * reader.GetPageSize(numberPage).Height);
                float lly = ury + (sap.Image.Height) * (Config.GetSizeImg() / 100);
                float llx = ((Config.GetY() / 100) * reader.GetPageSize(numberPage).Width);
                float urx = llx + (sap.Image.Width) * (Config.GetSizeImg() / 100);

                Rectangle rectagle = new Rectangle(llx, lly, urx, ury);
                sap.SetVisibleSignature(rectagle, numberPage, null);

            }

        }

        private void ConfigOutput()
        {
            if (!File.Exists(Input)) throw new Exception(string.Format("This file {0} not found.", Input));
            if (Config.GetOverlap())
            {
                Output = Input + ".output";
            }
            else
            {
                Output = Config.GetOutputFolder() + Input.Substring(Input.LastIndexOf("\\")); 
            }

        }

    }
}
