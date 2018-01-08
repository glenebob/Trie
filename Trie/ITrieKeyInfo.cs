using System.Collections.Generic;

namespace Trie
{
    /// <summary>
    /// Represents information about a key in a trie.
    /// </summary>
    public interface ITrieKeyInfo<TKey>
    {
        /// <summary>
        /// Creates a new storage container for a group of Trie nodes.
        /// </summary>
        /// <typeparam name="TNode">Node type.</typeparam>
        /// <returns>A new storage container with sufficient space for all distinct values of a key element.</returns>
        /// <remarks>This method is called by a Trie object whenever child node storage is needed.</remarks>
        ITrieNodeStorage<TNode> CreateTrieNodeStorage<TNode>() where TNode : ITrieNode;

        /// <summary>
        /// Enumerates the key space offset(s) of the element(s) of the current key.
        /// </summary>
        /// <returns>An enumerator set just before the first element in the key.</returns>
        IEnumerator<int> GetTrieNodeStorageIndexEnumerator(TKey key);
    }
}
