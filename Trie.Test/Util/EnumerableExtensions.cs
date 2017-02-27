using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Util
{
    public static class EnumerableExtensions
    {
        private class SkipController<T>
        {
            private int skip;
            private int skipCount;

            public SkipController(int skip)
            {
                this.skip = skip;
            }

            public bool Predicate(T o)
            {
                bool result = false;

                if (skipCount == 0)
                {
                    result = true;
                }

                skipCount++;

                if (skipCount > skip)
                {
                    skipCount = 0;
                }

                return result;
            }
        }

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

            if (skip == 0)
            {
                return source;
            }
            else
            {
                return source.Where(new SkipController<T>(skip).Predicate);
            }
        }
    }
}
