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
            return new Storage<TNode>();
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(UInt16 key)
        {
            for (int r = 14; r >= 0; r -= 2)
            {
                yield return (int)((key >> r) & 0b11);
            }
        }

        private class Storage<TNode> : ITrieNodeStorage<TNode>
        {
            private TNode value1;
            private TNode value2;
            private TNode value3;
            private TNode value4;

            public TNode this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return value1;
                        case 1: return value2;
                        case 2: return value3;
                        case 3: return value4;
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

                        case 2:
                            value3 = value;
                            break;

                        case 3:
                            value4 = value;
                            break;

                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }

            public int Length => 4;
        }
    }
}
