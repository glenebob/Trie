using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trie.Util;

namespace Trie.Test
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TakeSkipTest()
        {
            byte[] bytes = new byte[] { 1, 2, 3, 4, 5 };

            for (int skip = 0; skip <= 5; skip++)
            {
                int nextByte = 1;

                foreach (byte item in bytes.TakeSkip(skip))
                {
                    Assert.AreEqual(nextByte, item);
                    nextByte += skip + 1;
                }
            }
        }
    }
}
