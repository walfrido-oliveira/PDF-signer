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

        public Model.Config Config { get; set; }

        public Signer(string input, Model.Config config)
        {
            Input = input;
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

                if (Config.Overlap)
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

        public bool Signin(X509Certificate2 cert)
        {
            try
            {
                if (cert == null) return false;

                X509CertificateParser cp = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
                IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");

                PdfReader reader = new PdfReader(Input);
                PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(Output, FileMode.Create, FileAccess.Write), '\0', null, true);
                PdfSignatureAppearance sap = st.SignatureAppearance;
                ConfigPDFSignatureAppearance(sap, reader);

                MakeSignature.SignDetached(sap, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

                if (Config.Overlap)
                {
                    File.Delete(Input);
                    File.Copy(Output, Input);
                    File.Delete(Output);
                }

                st.Close();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ConfigPDFSignatureAppearance(PdfSignatureAppearance sap, PdfReader reader)
        {
            int numberPage = 0;

            if (Config.FirstPage) numberPage = 1;
            if (Config.LastPage) numberPage = reader.NumberOfPages;
            if (!Config.LastPage && !Config.FirstPage) numberPage = Config.NumberPage;
            
            if (Config.SignerVisible)
            {
                sap.Layer2Text = "";
                if (Config.TextSignerVisible)
                {
                    sap.Layer2Text = Config.SignerText;
                    sap.Layer2Font = new Font(Font.FontFamily.TIMES_ROMAN, Config.FontSize);
                }

                if (!File.Exists(Config.Img)) throw new Exception(string.Format("Image for signer not found.", Config.Img));
                sap.Image = Image.GetInstance(Config.Img);

                float ury = ((Config.X / 100) * reader.GetPageSize(numberPage).Height);
                float lly = ury + (sap.Image.Height) * (Config.SizeImg / 100);
                float llx = ((Config.Y / 100) * reader.GetPageSize(numberPage).Width);
                float urx = llx + (sap.Image.Width) * (Config.SizeImg / 100);

                Rectangle rectagle = new Rectangle(llx, lly, urx, ury);
                sap.SetVisibleSignature(rectagle, numberPage, null);

            }

        }

        private void ConfigOutput()
        {
            if (!File.Exists(Input)) throw new Exception(string.Format("This file {0} not found.", Input));
            if (Config.Overlap)
            {
                Output = Input + ".output";
            }
            else
            {
                Output = Config.OutputFolder + Input.Substring(Input.LastIndexOf("\\")); 
            }

        }

    }
}
