using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PDFSigner.Signer
{
    class SignerCollection
    {
        public List<Signer> Items { get; set; }

        public SignerCollection()
        {
            Items = new List<Signer>();
        }

        public void SigninColletion()
        {
            X509Certificate2 cert = ExternalSignature.GetExternalSignature();
            foreach(Signer i in Items)
            {
                i.Signin(cert);
            }

        }

    }
}
