using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Trie.Test
{
    [TestClass]
    public class TrieDictionaryExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullException()
        {
            new TrieDictionary<string, object>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddKeyValueThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Add(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddKeyValuePairThrowsArgumentException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Add(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveKeyThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveKeyValuePairThrowsArgumentException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Remove(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsKeyThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).ContainsKey(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContainsThrowsArgumentException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Contains(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSubTreeThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).GetSubTree(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptGetThrowsArgumentNullException()
        {
             var v = new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo())[null];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptSetThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo())[null] = null;
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void SubscriptGetThrowsKeyNotFoundException()
        {
             var v = new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo())[""];
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysAddThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Keys.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysRemoveThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Keys.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysClearThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Keys.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void KeysContainsThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Keys.Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void KeysGetSubTreeThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Keys.GetSubTree(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesAddThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Values.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesRemoveThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Values.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesClearThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(new StringAtoZTrieKeyInfo()).Values.Clear();
        }
    }
}
