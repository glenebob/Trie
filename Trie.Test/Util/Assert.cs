using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Trie.Util
{
    public static class TrieAssert
    {
        public static void Throws<E>(Action action, string message = "An expected exception was not thrown", bool includeDerived = false) where E : Exception
        {
            try
            {
                action();
            }
            catch (E e)
            {
                if (!includeDerived)
                {
                    if (!(e is E))
                    {
                        throw new AssertFailedException(message, e);
                    }
                }

                return;
            }

            throw new AssertFailedException(message);
        }
    }
}
