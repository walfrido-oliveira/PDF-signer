using System;
using System.Collections;

namespace PDFSigner.Model
{
    class ConfigColletion : CollectionBase
    {
        public Config this[int index]
        {
            get { return (Config)List[index]; }
            set { List[index] = value; }

        }

        public int IndexOf(Config item)
        {
            if (item != null)
            {
                return List.IndexOf(item);
            }
            return -1;
        }

        public void Add(Config item)
        {
            if (item != null)
            {
                List.Add(item);
            }
        }

        public void Remove(Config item)
        {
            if (InnerList != null)
            {
                InnerList.Remove(item);
            }
        }

        public void AdddRange(ConfigColletion collection)
        {
            if (collection != null)
            {
                InnerList.AddRange(collection);
            }
        }

        public void Insert(int index, Config item)
        {
            if (index <= List.Count && item != null)
            {
                List.Insert(index, item);
            }
        }

        public bool Contains(Config item)
        {
            return List.Contains(item);
        }


    }

}
