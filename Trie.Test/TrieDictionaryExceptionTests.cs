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
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Add(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddKeyValuePairThrowsArgumentException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Add(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveKeyThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveKeyValuePairThrowsArgumentException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Remove(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsKeyThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).ContainsKey(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContainsThrowsArgumentException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Contains(new KeyValuePair<string, object>(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSubTreeThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).GetSubTree(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptGetThrowsArgumentNullException()
        {
             _ = new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default)[null];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptSetThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default)[null] = null;
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void SubscriptGetThrowsKeyNotFoundException()
        {
             _ = new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default)[""];
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysAddThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Keys.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysRemoveThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Keys.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeysClearThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Keys.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void KeysContainsThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Keys.Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void KeysGetSubTreeThrowsArgumentNullException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Keys.GetSubTree(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesAddThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Values.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesRemoveThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Values.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuesClearThrowsInvalidOperationException()
        {
             new TrieDictionary<string, object>(StringAtoZTrieKeyInfo.Default).Values.Clear();
        }
    }
}
