using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class String0to9TrieKeyInfo : ITrieKeyInfo<string>
    {
        public static readonly ITrieKeyInfo<string> Default = new String0to9TrieKeyInfo();

        // Singleton.
        private String0to9TrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(10);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            foreach (int ch in key)
            {
                if (ch < '0' || ch > '9')
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                yield return ch - '0';
            }
        }
    }
}
