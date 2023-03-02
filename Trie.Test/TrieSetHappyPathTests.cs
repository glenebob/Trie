using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Trie.Test
{
    [TestClass]
    public class TrieSetHappyPathTests
    {
        [TestMethod]
        public void StringAtoZInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(StringAtoZTrieKeyInfo.Default, EnumerateTestStringsAtoZ().OrderBy(k => k, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void String0to9InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(String0to9TrieKeyInfo.Default, EnumerateTestStrings0to9().OrderBy(k => k, StringComparer.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void StringInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<string>(StringTrieKeyInfo.Default, EnumerateTestStringsAtoZ().OrderBy(k => k, StringComparer.Ordinal));
        }

        [TestMethod]
        public void UInt8InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<Byte>(UInt8BinaryTrieKeyInfo.Default, EnumerateTestUInt8s());
        }

        [TestMethod]
        public void UInt16InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt16>(UInt16HalfNibbleTrieKeyInfo.Default, EnumerateTestUInt16s());
        }

        [TestMethod]
        public void UInt32InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt32>(UInt32NibbleTrieKeyInfo.Default, EnumerateTestUInt32s());
        }

        [TestMethod]
        public void UInt64InsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<UInt64>(UInt64ByteTrieKeyInfo.Default, EnumerateTestUInt64s());
        }

        [TestMethod]
        public void ByteArrayInsertRemoveContainsCountMatch()
        {
            InsertRemoveContainsCountMatch<byte[]>(ByteArrayTrieKeyInfo.Default, EnumerateTestByteArrays());
        }

        [TestMethod]
        public void StringSubTreeEnumeration()
        {
            var trieSet = new TrieSet<string>(StringAtoZTrieKeyInfo.Default)
            {
                "",
                "aaa",
                "aaaa",
                "aaaaa",
                "aaaaaa",
                "b",
                "ba",
                "baa",
                "baaa",
                "baaaa",
                "baaaaa"
            };

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

        [TestMethod]
        public void HashSetVsTrieSetPopulateAndSearchBenchmark()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Trie.Test.Words.txt");
            var trieAtoZSet = new TrieSet<string>(StringAtoZTrieKeyInfo.Default);
            var trieSet = new TrieSet<string>(StringTrieKeyInfo.Default);
            var hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 8192, true))
            {
                string word;

                while ((word = reader.ReadLine()) != null)
                {
                    Assert.IsTrue(trieAtoZSet.Add(word));
                    Assert.IsTrue(trieSet.Add(word));
                    Assert.IsTrue(hashSet.Add(word));
                }
            }

            stream.Seek(0, SeekOrigin.Begin);

            var swTrieAtoZ = Stopwatch.StartNew();

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 8192, true))
            {
                string word;

                while ((word = reader.ReadLine()) != null)
                {
                    Assert.IsTrue(trieAtoZSet.Contains(word));
                }
            }

            swTrieAtoZ.Stop();
            stream.Seek(0, SeekOrigin.Begin);

            var swTrie = Stopwatch.StartNew();

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 8192, true))
            {
                string word;

                while ((word = reader.ReadLine()) != null)
                {
                    Assert.IsTrue(trieSet.Contains(word));
                }
            }

            swTrie.Stop();
            stream.Seek(0, SeekOrigin.Begin);

            var swHash = Stopwatch.StartNew();

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 8192, true))
            {
                string word;

                while ((word = reader.ReadLine()) != null)
                {
                    Assert.IsTrue(hashSet.Contains(word));
                }
            }

            swHash.Stop();

            using var writer = new StreamWriter("Benchmark.txt");

            writer.WriteLine($"Trie (A to Z, array storage) Set: {swTrieAtoZ.Elapsed}");
            writer.WriteLine($"Trie (arbitrary characters, dictionary storage) Set: {swTrie.Elapsed}");
            writer.WriteLine($"Hash Set: {swHash.Elapsed}");
        }

        private static void InsertRemoveContainsCountMatch<T>(ITrieKeyInfo<T> keyInfo, IEnumerable<T> testValues)
        {
            var trieSet = new TrieSet<T>(keyInfo);
            int count;

            Assert.AreEqual(0, trieSet.Count);

            testValues = testValues.ToArray();

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
