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
            new TrieSet<string>(new StringAtoZTrieKeyInfo()).Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveThrowsArgumentNullException()
        {
            new TrieSet<string>(new StringAtoZTrieKeyInfo()).Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsThrowsArgumentNullException()
        {
            new TrieSet<string>(new StringAtoZTrieKeyInfo()).Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSubTreeThrowsArgumentNullException()
        {
            new TrieSet<string>(new StringAtoZTrieKeyInfo()).GetSubTree(null);
        }
    }
}
