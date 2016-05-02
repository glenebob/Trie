using System.Collections.Generic;

namespace Trie
{
    /// <summary>
    /// Represents information about a key in a trie.
    /// </summary>
    public interface ITrieKeyInfo<TKey>
    {
        /// <summary>
        /// The size of the key space for a key element.
        /// </summary>
        int KeyElementSpace { get; }

        /// <summary>
        /// Enumerates the key space offset(s) of the element(s) of the current key.
        /// </summary>
        /// <returns>An enumerator set just before the first element in the key.</returns>
        IEnumerator<int> GetKeyElementSpaceIndexEnumerator(TKey key);
    }
}
