﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Trie.Util;

namespace Trie.Test
{
    [TestClass]
    public class TrieDictionaryHappyPathTests
    {
        [TestMethod]
        public void StringSingleCharacterInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(SingleItemTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestStringsSingleCharacter().OrderBy(k => k, StringComparer.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void StringAtoZInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(StringAtoZTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestStringsAtoZ().OrderBy(k => k, StringComparer.OrdinalIgnoreCase)));
        }

        public static void String0to9InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(String0to9TrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestStrings0to9().OrderBy(k => k, StringComparer.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void StringInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(StringTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestStringsAtoZ().OrderBy(k => k, StringComparer.Ordinal)));
        }

        [TestMethod]
        public void UInt8InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<Byte>(UInt8BinaryTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestUInt8s()));
        }

        [TestMethod]
        public void UInt16InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt16>(UInt16HalfNibbleTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestUInt16s()));
        }

        [TestMethod]
        public void UInt32InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt32>(UInt32NibbleTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestUInt32s()));
        }

        [TestMethod]
        public void UInt64InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt64>(UInt64ByteTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestUInt64s()));
        }

        [TestMethod]
        public void ByteArrayInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<byte[]>(ByteArrayTrieKeyInfo.Default, EnumerateTestValuePairs(EnumerateTestByteArrays()));
        }

        [TestMethod]
        public void StringSubTreeEnumeration()
        {
            var trieDict = new TrieDictionary<string, string>(StringAtoZTrieKeyInfo.Default)
            {
                { "", "" },
                { "aaa", "" },
                { "aaaa", "" },
                { "aaaaa", "" },
                { "aaaaaa", "" },
                { "b", "" },
                { "ba", "" },
                { "baa", "" },
                { "baaa", "" },
                { "baaaa", "" },
                { "baaaaa", "" }
            };

            Assert.AreEqual(11, trieDict.GetSubTree("").Count());
            Assert.AreEqual("", trieDict.GetSubTree("").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("").Last().Key);
            Assert.AreEqual("", trieDict.Keys.GetSubTree("").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("").Last());

            Assert.AreEqual(4, trieDict.GetSubTree("a").Count());
            Assert.AreEqual("aaa", trieDict.GetSubTree("a").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("a").Last().Key);
            Assert.AreEqual("aaa", trieDict.Keys.GetSubTree("a").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("a").Last());

            Assert.AreEqual(4, trieDict.GetSubTree("aa").Count());
            Assert.AreEqual("aaa", trieDict.GetSubTree("aa").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aa").Last().Key);
            Assert.AreEqual("aaa", trieDict.Keys.GetSubTree("aa").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aa").Last());

            Assert.AreEqual(4, trieDict.GetSubTree("aaa").Count());
            Assert.AreEqual("aaa", trieDict.GetSubTree("aaa").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aaa").Last().Key);
            Assert.AreEqual("aaa", trieDict.Keys.GetSubTree("aaa").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aaa").Last());

            Assert.AreEqual(3, trieDict.GetSubTree("aaaa").Count());
            Assert.AreEqual("aaaa", trieDict.GetSubTree("aaaa").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aaaa").Last().Key);
            Assert.AreEqual("aaaa", trieDict.Keys.GetSubTree("aaaa").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aaaa").Last());

            Assert.AreEqual(2, trieDict.GetSubTree("aaaaa").Count());
            Assert.AreEqual("aaaaa", trieDict.GetSubTree("aaaaa").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aaaaa").Last().Key);
            Assert.AreEqual("aaaaa", trieDict.Keys.GetSubTree("aaaaa").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aaaaa").Last());

            Assert.AreEqual(1, trieDict.GetSubTree("aaaaaa").Count());
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aaaaaa").First().Key);
            Assert.AreEqual("aaaaaa", trieDict.GetSubTree("aaaaaa").Last().Key);
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aaaaaa").First());
            Assert.AreEqual("aaaaaa", trieDict.Keys.GetSubTree("aaaaaa").Last());

            Assert.AreEqual(0, trieDict.GetSubTree("aaaaaaa").Count());

            Assert.AreEqual(6, trieDict.GetSubTree("b").Count());
            Assert.AreEqual("b", trieDict.GetSubTree("b").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("b").Last().Key);
            Assert.AreEqual("b", trieDict.Keys.GetSubTree("b").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("b").Last());

            Assert.AreEqual(5, trieDict.GetSubTree("ba").Count());
            Assert.AreEqual("ba", trieDict.GetSubTree("ba").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("ba").Last().Key);
            Assert.AreEqual("ba", trieDict.Keys.GetSubTree("ba").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("ba").Last());

            Assert.AreEqual(4, trieDict.GetSubTree("baa").Count());
            Assert.AreEqual("baa", trieDict.GetSubTree("baa").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("baa").Last().Key);
            Assert.AreEqual("baa", trieDict.Keys.GetSubTree("baa").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("baa").Last());

            Assert.AreEqual(3, trieDict.GetSubTree("baaa").Count());
            Assert.AreEqual("baaa", trieDict.GetSubTree("baaa").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("baaa").Last().Key);
            Assert.AreEqual("baaa", trieDict.Keys.GetSubTree("baaa").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("baaa").Last());

            Assert.AreEqual(2, trieDict.GetSubTree("baaaa").Count());
            Assert.AreEqual("baaaa", trieDict.GetSubTree("baaaa").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("baaaa").Last().Key);
            Assert.AreEqual("baaaa", trieDict.Keys.GetSubTree("baaaa").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("baaaa").Last());

            Assert.AreEqual(1, trieDict.GetSubTree("baaaaa").Count());
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("baaaaa").First().Key);
            Assert.AreEqual("baaaaa", trieDict.GetSubTree("baaaaa").Last().Key);
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("baaaaa").First());
            Assert.AreEqual("baaaaa", trieDict.Keys.GetSubTree("baaaaa").Last());

            Assert.AreEqual(0, trieDict.GetSubTree("baaaaaa").Count());

            Assert.AreEqual(0, trieDict.GetSubTree("c").Count());
        }

        private static void InsertRemoveContainsCountMatch<T>(ITrieKeyInfo<T> keyInfo, IEnumerable<KeyValuePair<T, T>> testValues)
        {
            var trieDict = new TrieDictionary<T, T>(keyInfo);
            int count;

            Assert.AreEqual(0, trieDict.Count);
            Assert.AreEqual(0, trieDict.Keys.Count);
            Assert.AreEqual(0, trieDict.Values.Count);

            testValues = testValues.ToArray();

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieDict.Contains(v));
            }

            count = 0;

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieDict.Contains(v));
                Assert.IsFalse(trieDict.ContainsKey(v.Key));
                Assert.IsFalse(trieDict.Keys.Contains(v.Key));
                trieDict.Add(v);
                Assert.IsTrue(trieDict.Contains(v));
                Assert.IsTrue(trieDict.ContainsKey(v.Key));
                Assert.IsTrue(trieDict.Keys.Contains(v.Key));
                count++;
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
                TrieAssert.Throws<ArgumentException>(() => trieDict.Add(v));
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
                Assert.IsTrue(trieDict.Contains(v));
                Assert.IsTrue(trieDict.ContainsKey(v.Key));
                Assert.IsTrue(trieDict.Keys.Contains(v.Key));
            }

            var expected = testValues.Select(v => v).GetEnumerator();
            var actualPairs = trieDict.GetEnumerator();

            while (expected.MoveNext())
            {
                Assert.IsTrue(actualPairs.MoveNext());
                Assert.AreEqual(expected.Current, actualPairs.Current);
            }

            Assert.IsFalse(actualPairs.MoveNext());

            expected = testValues.Select(v => v).GetEnumerator();
            var actualKeys = trieDict.Keys.GetEnumerator();

            while (expected.MoveNext())
            {
                Assert.IsTrue(actualKeys.MoveNext());
                Assert.AreEqual(expected.Current.Key, actualKeys.Current);
            }

            Assert.IsFalse(actualKeys.MoveNext());

            expected = testValues.GetEnumerator();
            var actualValues = trieDict.Values.GetEnumerator();

            while (expected.MoveNext())
            {
                Assert.IsTrue(actualValues.MoveNext());
                Assert.AreEqual(expected.Current.Value, actualValues.Current);
            }

            Assert.IsFalse(actualValues.MoveNext());

            foreach (var v in testValues)
            {
                Assert.IsTrue(trieDict.Remove(v));
                Assert.IsFalse(trieDict.Contains(v));
                Assert.IsFalse(trieDict.Keys.Contains(v.Key));
                count--;
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
                Assert.IsFalse(trieDict.Remove(v));
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
                Assert.IsFalse(trieDict.Contains(v));
                Assert.IsFalse(trieDict.Keys.Contains(v.Key));
                trieDict.Add(v);
                count++;
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
                Assert.IsTrue(trieDict.Remove(v));
                count--;
                Assert.AreEqual(count, trieDict.Count);
                Assert.AreEqual(count, trieDict.Keys.Count);
                Assert.AreEqual(count, trieDict.Values.Count);
            }

            foreach (var v in testValues)
            {
                trieDict.Add(v);
            }

            Assert.AreEqual(testValues.Count(), trieDict.Count);
            Assert.AreEqual(testValues.Count(), trieDict.Keys.Count);
            Assert.AreEqual(testValues.Count(), trieDict.Values.Count);
            trieDict.Clear();

            foreach (var v in testValues)
            {
                Assert.IsFalse(trieDict.Contains(v));
                Assert.IsFalse(trieDict.ContainsKey(v.Key));
                Assert.IsFalse(trieDict.Keys.Contains(v.Key));
            }

            Assert.AreEqual(0, trieDict.Count);
            Assert.AreEqual(0, trieDict.Keys.Count);
            Assert.AreEqual(0, trieDict.Values.Count);

            foreach (var v in testValues.TakeSkip(Math.Max(testValues.Count() / 100 - 1, 0)))
            {
                Assert.IsFalse(trieDict.Values.Contains(v.Value));
                trieDict.Add(v);
                Assert.IsTrue(trieDict.Values.Contains(v.Value));
            }
        }

        private static IEnumerable<KeyValuePair<T, T>> EnumerateTestValuePairs<T>(IEnumerable<T> enumerable)
        {
            enumerable = enumerable.ToArray();

            return Enumerable.Zip(enumerable, enumerable.Reverse(), (a, b) => new KeyValuePair<T, T>(a, b));
        }

        private static IEnumerable<string> EnumerateTestStringsSingleCharacter()
        {
            yield return "";
            yield return "0";
            yield return "00";
            yield return "000";
            yield return "0000";
            yield return "00000";
            yield return "000000";
            yield return "0000000";
            yield return "00000000";
            yield return "000000000000000000";
            yield return "0000000000000000000000000000000000";
            yield return "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
        }

        private static IEnumerable<string> EnumerateTestStringsAtoZ()
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

        private static IEnumerable<string> EnumerateTestStrings0to9()
        {
            yield return "";
            yield return "0";
            yield return "9";
            yield return "1";
            yield return "8";
            yield return "22";
            yield return "77";
            yield return "0123456789";
            yield return "9876543210";
            yield return "012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321012345678987654321";
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

        private static IEnumerable<byte[]> EnumerateTestByteArrays()
        {
            yield return CreateRangedByteArray(0, 0);
            yield return CreateRangedByteArray(1, 1);
            yield return CreateRangedByteArray(2, 1);
            yield return CreateRangedByteArray(3, 4);
            yield return CreateRangedByteArray(7, 4);
            yield return CreateRangedByteArray(11, 10);
            yield return CreateRangedByteArray(21, 10);
            yield return CreateRangedByteArray(31, 200);
        }

        private static byte[] CreateRangedByteArray(byte start, int count)
        {
            return Enumerable
                .Range(start, count)
                .Select((i) => (byte)i)
                .ToArray();
        }
    }
}
