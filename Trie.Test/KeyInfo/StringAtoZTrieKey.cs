using System;
using System.Collections.Generic;

namespace Trie.Test
{
    public class StringAtoZTrieKeyInfo : ITrieKeyInfo<string>
    {
        public static readonly ITrieKeyInfo<string> Default = new StringAtoZTrieKeyInfo();

        public int KeyElementSpace
        {
            get { return 26; }
        }

        public IEnumerator<int> GetKeyElementSpaceIndexEnumerator(string key)
        {
            foreach (char ch in key)
            {
                if (ch >= 'A' && ch <= 'Z')
                {
                    yield return ch - 'A';
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    yield return ch - 'a';
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }
            }
        }
    }
}
