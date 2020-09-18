using System.Security.Cryptography.X509Certificates;

namespace PDFSigner.Signer
{
    class ExternalSignature
    {
        public static X509Certificate2 GetExternalSignature()
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);

            if (sel.Count > 0) return sel[0];
            throw new System.Exception("Certicate not be null"); 
        }

    }
}
