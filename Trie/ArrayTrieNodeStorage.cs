namespace Trie
{
    /// <summary>
    /// Represents trie node storage which is backed by a System.Array.
    /// </summary>
    public class ArrayTrieNodeStorage<TNode> : ITrieNodeStorage<TNode>
    {
        private readonly TNode[] storage;

        public ArrayTrieNodeStorage(int size)
        {
            this.storage = new TNode[size];
        }

        public TNode this[int index]
        {
            get { return storage[index]; }
            set { storage[index] = value; }
        }

        public int Length
        {
            get { return storage.Length; }
        }
    }
}
