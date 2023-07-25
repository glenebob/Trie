using System;
using System.Collections.Generic;

namespace Trie
{
    /// <summary>
    /// Represents trie node storage which is backed by a System.Collections.Generic.Dictionary.
    /// </summary>
    /// <remarks>This storage class is most useful for a trie with a wide key element space
    /// which is expected to be sparsely populated.</remarks>
    public class DictionaryTrieNodeStorage<TNode> : ITrieNodeStorage<TNode>
    {
        private readonly Dictionary<int, TNode> storage;
        private readonly int size;

        public DictionaryTrieNodeStorage(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            this.storage = new Dictionary<int, TNode>();
            this.size = size;
        }

        public TNode this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                if (storage.TryGetValue(index, out TNode node))
                {
                    return node;
                } 
                else
                {
                    return default;
                }
            }

            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                storage[index] = value;
            }
        }

        public int Length
        {
            get { return size; }
        }
    }
}
