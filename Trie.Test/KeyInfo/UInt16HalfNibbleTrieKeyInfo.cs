using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt16HalfNibbleTrieKeyInfo : ITrieKeyInfo<UInt16>
    {
        public static readonly ITrieKeyInfo<UInt16> Default = new UInt16HalfNibbleTrieKeyInfo();

        // Singleton.
        private UInt16HalfNibbleTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(4);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(UInt16 key)
        {
            for (int r = 14; r >= 0; r -= 2)
            {
                yield return (int)((key >> r) & 0b11);
            }
        }
    }
}
