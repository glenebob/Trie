using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt8BinaryTrieKeyInfo : ITrieKeyInfo<Byte>
    {
        public static readonly ITrieKeyInfo<Byte> Default = new UInt8BinaryTrieKeyInfo();

        // Singleton.
        private UInt8BinaryTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(2);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(Byte key)
        {
            for (int r = 7; r >= 0; r -= 1)
            {
                yield return (int)((key >> r) & 1);
            }
        }
    }
}
