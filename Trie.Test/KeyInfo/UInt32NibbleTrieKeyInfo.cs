using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt32NibbleTrieKeyInfo : ITrieKeyInfo<UInt32>
    {
        public static readonly ITrieKeyInfo<UInt32> Default = new UInt32NibbleTrieKeyInfo();

        // Singleton.
        private UInt32NibbleTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(16);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(UInt32 key)
        {
            for (int r = 28; r >= 0; r -= 4)
            {
                yield return (int)((key >> r) & 0xF);
            }
        }
    }
}
