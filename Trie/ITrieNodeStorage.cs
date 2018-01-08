namespace Trie
{
    /// <summary>
    /// Represents storage for a group of trie nodes.
    /// </summary>
    /// <typeparam name="TNode">The type of the trie node.</typeparam>
    public interface ITrieNodeStorage<TNode>
    {
        /// <summary>
        /// Get or set the trie node at a given index.
        /// </summary>
        /// <param name="index">The index within the node storage.</param>
        TNode this[int index] { get; set; }

        /// <summary>
        /// Reports the size of the storage.
        /// </summary>
        int Length { get; }
    }
}
