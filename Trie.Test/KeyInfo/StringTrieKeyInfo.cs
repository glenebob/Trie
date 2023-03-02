using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Test
{
    public class StringTrieKeyInfo : ITrieKeyInfo<string>
    {
        public static readonly ITrieKeyInfo<string> Default = new StringTrieKeyInfo();

        // Singleton.
        private StringTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new DictionaryTrieNodeStorage<TNode>(char.MaxValue + 1);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return key
                .Select(ch => (int)ch)
                .GetEnumerator();
        }
    }
}
