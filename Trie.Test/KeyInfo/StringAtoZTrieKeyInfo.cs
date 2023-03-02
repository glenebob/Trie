using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class StringAtoZTrieKeyInfo : ITrieKeyInfo<string>
    {
        private const byte F = 0xFF;

        private static readonly byte[] CharacterToSpaceIndexMap = new byte[]
        {
            F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, // Not a letter
            F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, // Not a letter
            F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, // Not a letter
            F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, // Not a letter
            F, // Not a letter
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, // A - Z
            F, F, F, 17, 18, 19, // Not a letter
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25  // a - z
        };

        public static readonly ITrieKeyInfo<string> Default = new StringAtoZTrieKeyInfo();

        // Singleton.
        private StringAtoZTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new ArrayTrieNodeStorage<TNode>(26);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            foreach (int ch in key)
            {
                if (ch >= CharacterToSpaceIndexMap.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                byte index = CharacterToSpaceIndexMap[ch];

                if (index == F)
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                yield return index;
            }
        }
    }
}
