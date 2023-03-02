using System;
using System.Collections.Generic;

namespace Trie
{
    public class TrieSet<TKey> : TrieSetNode<TKey>
    {
        private readonly ITrieKeyInfo<TKey> keyInfo;

        public TrieSet(ITrieKeyInfo<TKey> keyInfo)
        {
            if (keyInfo == null)
            {
                throw new ArgumentNullException(nameof(keyInfo));
            }

            this.keyInfo = keyInfo;
        }

        public int Count
        {
            get;
            private set;
        }

        public bool Add(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (base.Add(key, this.keyInfo, this.keyInfo.GetTrieNodeStorageIndexEnumerator(key)))
            {
                Count++;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (base.Remove(this.keyInfo.GetTrieNodeStorageIndexEnumerator(key)))
            {
                Count--;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this.Count == 0)
            {
                return false;
            }

            return base.Contains(this.keyInfo.GetTrieNodeStorageIndexEnumerator(key));
        }

        public new void Clear()
        {
            base.Clear();
            this.Count = 0;
        }

        public IEnumerable<TKey> GetSubTree(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return base.GetSubTree(this.keyInfo.GetTrieNodeStorageIndexEnumerator(key));
        }
    }
}
