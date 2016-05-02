using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class UInt8BinaryTrieKeyInfo : ITrieKeyInfo<Byte>
    {
        public static readonly ITrieKeyInfo<Byte> Default = new UInt8BinaryTrieKeyInfo();

        public int KeyElementSpace
        {
            get { return 2; }
        }

        public IEnumerator<int> GetKeyElementSpaceIndexEnumerator(Byte key)
        {
            for (int r = 7; r >= 0; r -= 1)
            {
                yield return (int)((key >> r) & 0x01);
            }
        }
    }
}
