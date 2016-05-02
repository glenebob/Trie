using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    public class TrieDictionary<TKey, TValue> : TrieDictionaryNode<TKey, TValue>, IDictionary<TKey, TValue>
    {
        private readonly ITrieKeyInfo<TKey> keyInfo;

        private ProxyKeyCollection<TKey> keyCollectionProxy;
        private ProxyCollection<TValue> valueCollectionProxy;

        public TrieDictionary(ITrieKeyInfo<TKey> keyInfo)
        {
            if (keyInfo == null)
            {
                throw new ArgumentNullException(nameof(keyInfo));
            }

            this.keyInfo = keyInfo;
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return this.Keys; }
        }

        public ProxyKeyCollection<TKey> Keys
        {
            get
            {
                return
                    this.keyCollectionProxy ?? (this.keyCollectionProxy =
                        new ProxyKeyCollection<TKey>(
                            this,
                            () => this.Select((kvp) => kvp.Key),
                            (key) => this.ContainsKey(key)));
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return this.Values; }
        }

        public ProxyCollection<TValue> Values
        {
            get
            {
                return
                    this.valueCollectionProxy ?? (this.valueCollectionProxy =
                        new ProxyCollection<TValue>(
                            this,
                            () => this.Select((kvp) => kvp.Value),
                            (value) => this.Select((kvp) => kvp.Value).Contains(value)));
            }
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                TValue value;

                if (!TryGetValue(this.keyInfo.GetKeyElementSpaceIndexEnumerator(key), out value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (SetOrAdd(new KeyValuePair<TKey, TValue>(key, value), this.keyInfo.KeyElementSpace, this.keyInfo.GetKeyElementSpaceIndexEnumerator(key), true))
                {
                    this.Count++;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            if (pair.Key == null)
            {
                throw new ArgumentNullException(nameof(pair.Key));
            }

            if (base.SetOrAdd(pair, this.keyInfo.KeyElementSpace, this.keyInfo.GetKeyElementSpaceIndexEnumerator(pair.Key), false))
            {
                Count++;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (base.Remove(this.keyInfo.GetKeyElementSpaceIndexEnumerator(key)))
            {
                Count--;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            return this.Remove(pair.Key);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this.Count == 0)
            {
                return false;
            }

            return base.Contains(this.keyInfo.GetKeyElementSpaceIndexEnumerator(key));
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            return this.ContainsKey(pair.Key);
        }

        public new void Clear()
        {
            base.Clear();
            this.Count = 0;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return base.TryGetValue(keyInfo.GetKeyElementSpaceIndexEnumerator(key), out value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] destination, int offset)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            foreach (KeyValuePair<TKey, TValue> item in this)
            {
                destination[offset++] = item;
            }
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetSubTree(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return base.GetSubTree(this.keyInfo.GetKeyElementSpaceIndexEnumerator(key));
        }

        public class ProxyCollection<T> : ICollection<T>
        {
            private readonly Func<IEnumerable<T>> enumerableFactory;
            private readonly Func<T, bool> contains;

            protected readonly TrieDictionary<TKey, TValue> parent;

            public ProxyCollection(TrieDictionary<TKey, TValue> parent, Func<IEnumerable<T>> enumerableFactory, Func<T, bool> contains)
            {
                this.parent = parent;
                this.enumerableFactory = enumerableFactory;
                this.contains = contains;
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public int Count
            {
                get { return parent.Count; }
            }

            public void Add(T key)
            {
                throw new InvalidOperationException();
            }

            public bool Remove(T key)
            {
                throw new InvalidOperationException();
            }

            public void Clear()
            {
                throw new InvalidOperationException();
            }

            public bool Contains(T value)
            {
                return this.contains(value);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return this.enumerableFactory().GetEnumerator();
            }

            public void CopyTo(T[] destination, int offset)
            {
                foreach (T item in this)
                {
                    destination[offset++] = item;
                }
            }
        }

        public class ProxyKeyCollection<T> : ProxyCollection<T>
        {
            public ProxyKeyCollection(TrieDictionary<TKey, TValue> parent, Func<IEnumerable<T>> enumerableFactory, Func<T, bool> contains)
            : base(parent, enumerableFactory, contains)
            { }

            public IEnumerable<TKey> GetSubTree(TKey key)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return this.parent.GetSubTree(key).Select((kvp) => kvp.Key);
            }
        }
    }
}
