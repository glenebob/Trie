using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt64ByteTrieKeyInfo : ITrieKeyInfo<UInt64>
    {
        public static readonly ITrieKeyInfo<UInt64> Default = new UInt64ByteTrieKeyInfo();

        public int KeyElementSpace
        {
            get { return 256; }
        }

        public IEnumerator<int> GetKeyElementSpaceIndexEnumerator(UInt64 key)
        {
            for (int r = 56; r >= 0; r -= 8)
            {
                yield return (int)((key >> r) & 0xFF);
            }
        }
    }
}
