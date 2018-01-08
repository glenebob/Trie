using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Trie.Test
{
    [TestClass]
    public class TrieSetExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullException()
        {
            new TrieSet<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullException()
        {
            new TrieSet<string>(StringAtoZTrieKeyInfo.Default).Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveThrowsArgumentNullException()
        {
            new TrieSet<string>(StringAtoZTrieKeyInfo.Default).Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsThrowsArgumentNullException()
        {
            new TrieSet<string>(StringAtoZTrieKeyInfo.Default).Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSubTreeThrowsArgumentNullException()
        {
            new TrieSet<string>(StringAtoZTrieKeyInfo.Default).GetSubTree(null);
        }
    }
}
