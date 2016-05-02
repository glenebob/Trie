using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt16HalfNibbleTrieKeyInfo : ITrieKeyInfo<UInt16>
    {
        public static readonly ITrieKeyInfo<UInt16> Default = new UInt16HalfNibbleTrieKeyInfo();

        public int KeyElementSpace
        {
            get { return 4; }
        }

        public IEnumerator<int> GetKeyElementSpaceIndexEnumerator(UInt16 key)
        {
            for (int r = 14; r >= 0; r -= 2)
            {
                yield return (int)((key >> r) & 0x03);
            }
        }
    }
}
