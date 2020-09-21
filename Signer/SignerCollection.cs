using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace PDFSigner.Signer
{
    class SignerCollection : CollectionBase
    {

        public Signer this[int index]
        {
            get { return (Signer)List[index];  }
            set { List[index] = value; }
        }

        public void Signin()
        {
            X509Certificate2 cert = ExternalSignature.GetExternalSignature();
            foreach(Signer i in List)
            {
                i.Signin(cert);
            }

        }

        public int IndexOf(Signer item)
        {
            if (item != null)
            {
                return List.IndexOf(item);
            }
            return -1;
        }

        public void Add(Signer item)
        {
            if (item != null)
            {
                List.Add(item);
            }
        }

        public void Remove(Signer item)
        {
            if (InnerList != null)
            {
                InnerList.Remove(item);
            }
        }

        public void AdddRange(SignerCollection collection)
        {
            if (collection != null)
            {
                InnerList.AddRange(collection);
            }
        }

        public void Insert(int index, Signer item)
        {
            if (index <= List.Count && item != null)
            {
                List.Insert(index, item);
            }
        }

        public bool Contains(Signer item)
        {
            return List.Contains(item);
        }
    }
}
