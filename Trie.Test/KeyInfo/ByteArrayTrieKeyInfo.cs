using System.Collections.Generic;

namespace Trie.Test
{
    public class ByteArrayTrieKeyInfo : ITrieKeyInfo<byte[]>
    {
        public static readonly ITrieKeyInfo<byte[]> Default = new ByteArrayTrieKeyInfo();

        // Singleton.
        private ByteArrayTrieKeyInfo() { }

        public ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode
        {
            return new DictionaryTrieNodeStorage<TNode>(byte.MaxValue + 1);
        }

        public IEnumerator<int> GetTrieNodeStorageIndexEnumerator(byte[] key)
        {
            foreach (byte item in key)
            {
                yield return item;
            }
        }
    }
}
