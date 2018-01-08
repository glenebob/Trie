using System;
using System.Collections.Generic;

namespace Trie.Util
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Enumerates the items in a sequence, skipping some, beginning with the first item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source enumeration.</param>
        /// <param name="skip">The number of items to skip after each item enumerated.</param>
        /// <returns></returns>
        public static IEnumerable<T> TakeSkip<T>(this IEnumerable<T> source, int skip)
        {
            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip));
            }

            int skipped = skip;

            foreach (T item in source)
            {
                if (skipped == skip)
                {
                    yield return item;
                    skipped = 0;
                }
                else
                {
                    skipped++;
                }
            }
        }
    }
}
