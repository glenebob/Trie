using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class SingleItemTrieKeyInfo : ITrieKeyInfo<string>
    {
        public static readonly ITrieKeyInfo<string> Default = new SingleItemTrieKeyInfo();

        // Singleton.
        private SingleItemTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(1);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(string key)
        {
            foreach (int ch in key)
            {
                if (ch != '0')
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                yield return ch - '0';
            }
        }
    }
}
