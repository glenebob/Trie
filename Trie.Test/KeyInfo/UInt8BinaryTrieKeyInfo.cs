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
            return new Storage<TNode>();
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(Byte key)
        {
            for (int r = 7; r >= 0; r -= 1)
            {
                yield return (int)((key >> r) & 1);
            }
        }

        private class Storage<TNode> : ITrieNodeStorage<TNode>
        {
            private TNode value1;
            private TNode value2;

            public TNode this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return value1;
                        case 1: return value2;
                        default: throw new IndexOutOfRangeException();
                    }
                }

                set
                {
                    switch (index)
                    {
                        case 0:
                            value1 = value;
                            break;

                        case 1:
                            value2 = value;
                            break;

                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }

            public int Length => 2;
        }
    }
}
