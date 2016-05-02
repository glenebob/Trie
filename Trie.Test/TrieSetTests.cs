using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Trie.Util;

namespace Trie.Test
{
    [TestClass]
    public class TrySetTests
    {
        [TestMethod]
        public void ThrowsExpectedExceptions()
        {
            TrieAssert.Throws<ArgumentNullException>(() => new TrieSet<string>(null));

            var triSet = new TrieSet<string>(new StringAtoZTrieKeyInfo());

            TrieAssert.Throws<ArgumentNullException>(() => triSet.Add(null));
            TrieAssert.Throws<ArgumentNullException>(() => triSet.Remove(null));
            TrieAssert.Throws<ArgumentNullException>(() => triSet.Contains(null));
            TrieAssert.Throws<ArgumentNullException>(() => triSet.GetSubTree(null));
        }

        [TestMethod]
        public void StringInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(StringAtoZTrieKeyInfo.Default, EnumerateTestStrings);
        }

        [TestMethod]
        public void UInt8InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<Byte>(UInt8BinaryTrieKeyInfo.Default, EnumerateTestUInt8s);
        }

        [TestMethod]
        public void UInt16InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt16>(UInt16HalfNibbleTrieKeyInfo.Default, EnumerateTestUInt16s);
        }

        [TestMethod]
        public void UInt32InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt32>(UInt32NibbleTrieKeyInfo.Default, EnumerateTestUInt32s);
        }

        [TestMethod]
        public void UInt64InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt64>(new UInt64ByteTrieKeyInfo(), EnumerateTestUInt64s);
        }

        [TestMethod]
        public void StringSubTreeEnumeration()
        {
            var trieSet = new TrieSet<string>(StringAtoZTrieKeyInfo.Default);

            trieSet.Add("");
            trieSet.Add("aaa");
            trieSet.Add("aaaa");
            trieSet.Add("aaaaa");
            trieSet.Add("aaaaaa");
            trieSet.Add("b");
            trieSet.Add("ba");
            trieSet.Add("baa");
            trieSet.Add("baaa");
            trieSet.Add("baaaa");
            trieSet.Add("baaaaa");

            Assert.AreEqual(11, trieSet.GetSubTree("").Count());
            Assert.AreEqual("", trieSet.GetSubTree("").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("").Last());

            Assert.AreEqual(4, trieSet.GetSubTree("a").Count());
            Assert.AreEqual("aaa", trieSet.GetSubTree("a").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("a").Last());

            Assert.AreEqual(4, trieSet.GetSubTree("aa").Count());
            Assert.AreEqual("aaa", trieSet.GetSubTree("aa").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aa").Last());

            Assert.AreEqual(4, trieSet.GetSubTree("aaa").Count());
            Assert.AreEqual("aaa", trieSet.GetSubTree("aaa").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aaa").Last());

            Assert.AreEqual(3, trieSet.GetSubTree("aaaa").Count());
            Assert.AreEqual("aaaa", trieSet.GetSubTree("aaaa").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aaaa").Last());

            Assert.AreEqual(2, trieSet.GetSubTree("aaaaa").Count());
            Assert.AreEqual("aaaaa", trieSet.GetSubTree("aaaaa").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aaaaa").Last());

            Assert.AreEqual(1, trieSet.GetSubTree("aaaaaa").Count());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aaaaaa").First());
            Assert.AreEqual("aaaaaa", trieSet.GetSubTree("aaaaaa").Last());

            Assert.AreEqual(0, trieSet.GetSubTree("aaaaaaa").Count());

            Assert.AreEqual(6, trieSet.GetSubTree("b").Count());
            Assert.AreEqual("b", trieSet.GetSubTree("b").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("b").Last());

            Assert.AreEqual(5, trieSet.GetSubTree("ba").Count());
            Assert.AreEqual("ba", trieSet.GetSubTree("ba").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("ba").Last());

            Assert.AreEqual(4, trieSet.GetSubTree("baa").Count());
            Assert.AreEqual("baa", trieSet.GetSubTree("baa").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("baa").Last());

            Assert.AreEqual(3, trieSet.GetSubTree("baaa").Count());
            Assert.AreEqual("baaa", trieSet.GetSubTree("baaa").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("baaa").Last());

            Assert.AreEqual(2, trieSet.GetSubTree("baaaa").Count());
            Assert.AreEqual("baaaa", trieSet.GetSubTree("baaaa").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("baaaa").Last());

            Assert.AreEqual(1, trieSet.GetSubTree("baaaaa").Count());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("baaaaa").First());
            Assert.AreEqual("baaaaa", trieSet.GetSubTree("baaaaa").Last());

            Assert.AreEqual(0, trieSet.GetSubTree("baaaaaa").Count());

            Assert.AreEqual(0, trieSet.GetSubTree("c").Count());
        }

        private void InsertRemoveContainsCountMatch<T>(ITrieKeyInfo<T> keyInfo, Func<IEnumerable<T>> enumerateTestValues)
        {
            var testValues = enumerateTestValues().OrderBy(k => k).ToArray();
            var trieSet = new TrieSet<T>(keyInfo);
            int count;

            Assert.AreEqual(0, trieSet.Count);
            Assert.AreEqual(0, trieSet.Count());

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieSet.Contains(v));
            }

            count = 0;

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieSet.Contains(v));
                Assert.IsTrue(trieSet.Add(v));
                Assert.IsTrue(trieSet.Contains(v));
                count++;
                Assert.AreEqual(count, trieSet.Count);
                Assert.IsFalse(trieSet.Add(v));
                Assert.AreEqual(count, trieSet.Count);
                Assert.IsTrue(trieSet.Contains(v));
            }

            var expected = testValues.GetEnumerator();
            var actual = trieSet.GetEnumerator();

            while (expected.MoveNext())
            {
                Assert.IsTrue(actual.MoveNext());
                Assert.AreEqual(expected.Current, actual.Current);
            }

            Assert.IsFalse(actual.MoveNext());

            foreach (var v in testValues)
            {
                Assert.IsTrue(trieSet.Remove(v));
                Assert.IsFalse(trieSet.Contains(v));
                count--;
                Assert.AreEqual(count, trieSet.Count);
                Assert.IsFalse(trieSet.Remove(v));
                Assert.AreEqual(count, trieSet.Count);
                Assert.IsFalse(trieSet.Contains(v));
                Assert.IsTrue(trieSet.Add(v));
                count++;
                Assert.AreEqual(count, trieSet.Count);
                Assert.IsTrue(trieSet.Remove(v));
                count--;
                Assert.AreEqual(count, trieSet.Count);
            }

            foreach (var v in testValues)
            {
                Assert.IsTrue(trieSet.Add(v));
            }

            Assert.AreEqual(testValues.Count(), trieSet.Count);
            trieSet.Clear();

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieSet.Contains(v));
            }

            Assert.AreEqual(0, trieSet.Count);
            Assert.AreEqual(0, trieSet.Count());
        }

        private static IEnumerable<string> EnumerateTestStrings()
        {
            yield return "";
            yield return "a";
            yield return "z";
            yield return "B";
            yield return "Y";
            yield return "cc";
            yield return "yy";
            yield return "DD";
            yield return "XX";
            yield return "abcdefghijklmnopqrstuvwxyz";
            yield return "ZYXWVUTSRQPONMLKJIHGFEDCBA";
            yield return "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        private static IEnumerable<Byte> EnumerateTestUInt8s()
        {
            for (int i = 0 ; i <= Byte.MaxValue; i++)
            {
                yield return (Byte)i;
            }
        }

        private static IEnumerable<UInt16> EnumerateTestUInt16s()
        {
            for (UInt64 i = 0 ; i <= UInt16.MaxValue; i++)
            {
                yield return (UInt16)i;
            }
        }

        private static IEnumerable<UInt32> EnumerateTestUInt32s()
        {
            for (UInt64 i = 0 ; i <= UInt32.MaxValue; i += 0xFFFA)
            {
                yield return (UInt32)i;
            }
        }

        private static IEnumerable<UInt64> EnumerateTestUInt64s()
        {
            for (double i = 0 ; i <= UInt64.MaxValue; i += 0xFFFFFFFFFFFA)
            {
                yield return (UInt64)i;
            }
        }
    }
}
