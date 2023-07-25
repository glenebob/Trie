using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Trie.Test
{
    [TestClass]
    public class StorageExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DictionaryStorageCTorThrowsOnSizeLessThanOne()
        {
            var s = new DictionaryTrieNodeStorage<int>(0);

            s[-1] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DictionaryStorageThrowsOnIndexLessThanZero()
        {
            var s = new DictionaryTrieNodeStorage<int>(1);

            s[-1] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DictionaryStorageThrowsOnIndexGreaterThanLength()
        {
            var s = new DictionaryTrieNodeStorage<int>(1);

            s[1] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ArrayStorageCTorThrowsOnSizeLessThanOne()
        {
            var s = new ArrayTrieNodeStorage<int>(0);

            s[-1] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ArrayStorageThrowsOnIndexLessThanZero()
        {
            var s = new ArrayTrieNodeStorage<int>(1);

            s[-1] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ArrayStorageThrowsOnIndexGreaterThanLength()
        {
            var s = new ArrayTrieNodeStorage<int>(1);

            s[1] = 0;
        }
    }
}
