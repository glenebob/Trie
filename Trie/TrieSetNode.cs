using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    public class TrieSetNode<TKey> : ITrieNode, IEnumerable<TKey>
    {
        private ITrieNodeStorage<TrieSetNode<TKey>> children;
        private int childCount;
        private TKey key;
        private bool keyPopulated;

        protected TrieSetNode()
        { }

        protected bool Add(TKey key, ITrieKeyInfo<TKey> keyInfo, IEnumerator<int> keyElementSpaceIndexEnumerator)
        {
            TrieSetNode<TKey> node = this;
            TrieSetNode<TKey> child;

            while (keyElementSpaceIndexEnumerator.MoveNext())
            {
                if (node.children == null)
                {
                    node.children = keyInfo.CreateTrieNodeStorage<TrieSetNode<TKey>>();
                    child = null;
                }
                else
                {
                    child = node.children[keyElementSpaceIndexEnumerator.Current];
                }

                if (child == null)
                {
                    child = new TrieSetNode<TKey>();

                    node.children[keyElementSpaceIndexEnumerator.Current] = child;
                    node.childCount++;
                }

                node = child;
            }

            if (!node.keyPopulated)
            {
                node.key = key;
                node.keyPopulated = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool Remove(IEnumerator<int> keyElementSpaceIndexEnumerator)
        {
            if (!keyElementSpaceIndexEnumerator.MoveNext())
            {
                if (this.keyPopulated)
                {
                    this.key = default;
                    this.keyPopulated = false;

                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (children == null)
            {
                return false;
            }

            int index;
            TrieSetNode<TKey> child;

            index = keyElementSpaceIndexEnumerator.Current;
            child = children[index];

            if (child == null)
            {
                return false;
            }

            bool removed;

            removed = child.Remove(keyElementSpaceIndexEnumerator);

            if (removed)
            {
                if (!child.keyPopulated && child.children == null)
                {
                    children[index] = null;
                    childCount--;

                    if (childCount == 0)
                    {
                        children = null;
                    }
                }
            }

            return removed;
        }

        private TrieSetNode<TKey> TryGetNode(IEnumerator<int> keyElementSpaceIndexEnumerator)
        {
            TrieSetNode<TKey> node = this;

            while (keyElementSpaceIndexEnumerator.MoveNext())
            {
                if (node.children == null)
                {
                    return null;
                }

                node = node.children[keyElementSpaceIndexEnumerator.Current];

                if (node == null)
                {
                    return null;
                }
            }

            return node;
        }

        protected void Clear()
        {
            this.childCount = 0;
            this.children = null;
            this.key = default;
            this.keyPopulated = false;
        }

        protected bool Contains(IEnumerator<int> enumerator)
        {
            TrieSetNode<TKey> node = this.TryGetNode(enumerator);

            return node != null && node.keyPopulated;
        }

        protected IEnumerable<TKey> GetSubTree(IEnumerator<int> enumerator)
        {
            TrieSetNode<TKey> node = this.TryGetNode(enumerator);

            if (node != null)
            {
                return node;
            }
            else
            {
                return Enumerable.Empty<TKey>();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            if (this.keyPopulated)
            {
                yield return this.key;
            }

            if (this.childCount == 0)
            {
                yield break;
            }

            Stack<NodeCurser> stack = new Stack<NodeCurser>();

            stack.Push(new NodeCurser(this));

            while (stack.Count > 0)
            {
                NodeCurser cursor = stack.Peek();
                TrieSetNode<TKey> node = null;

                if (cursor.Node.children != null)
                {
                    for ( ; cursor.ChildIndex < cursor.Node.children.Length && node == null ; cursor.ChildIndex++)
                    {
                        node = cursor.Node.children[cursor.ChildIndex];
                    }
                }

                if (node != null)
                {
                    stack.Push(new NodeCurser(node));

                    if (node.keyPopulated)
                    {
                        yield return node.key;
                    }
                }
                else
                {
                    stack.Pop();
                }
            }
        }

        private class NodeCurser
        {
            public TrieSetNode<TKey> Node;
            public int ChildIndex;

            public NodeCurser(TrieSetNode<TKey> node)
            {
                this.Node = node;
                this.ChildIndex = 0;
            }
        }
    }
}
