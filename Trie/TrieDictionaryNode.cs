using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    public class TrieDictionaryNode<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private TrieDictionaryNode<TKey, TValue>[] children;
        private int childCount;
        private KeyValuePair<TKey, TValue>? keyValuePair;

        protected TrieDictionaryNode()
        { }

        protected bool SetOrAdd(KeyValuePair<TKey, TValue> pair, int keySpace, IEnumerator<int> enumerator, bool overwrite)
        {
            TrieDictionaryNode<TKey, TValue> node = this;
            TrieDictionaryNode<TKey, TValue> child;

            while (enumerator.MoveNext())
            {
                if (node.children == null)
                {
                    node.children = new TrieDictionaryNode<TKey, TValue>[keySpace];
                    child = null;
                }
                else
                {
                    child = node.children[enumerator.Current];
                }

                if (child == null)
                {
                    child = new TrieDictionaryNode<TKey, TValue>();

                    node.children[enumerator.Current] = child;
                    node.childCount++;
                }

                node = child;
            }

            bool add = !node.keyValuePair.HasValue;

            if (!node.keyValuePair.HasValue || overwrite)
            {
                node.keyValuePair = pair;

                return add;
            }
            else
            {
                throw new ArgumentException("An item with the same key has already been added");
            }
        }

        protected bool Remove(IEnumerator<int> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                if (this.keyValuePair.HasValue)
                {
                    this.keyValuePair = null;

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
            TrieDictionaryNode<TKey, TValue> child;

            index = enumerator.Current;
            child = children[index];

            if (child == null)
            {
                return false;
            }

            bool removed;

            removed = child.Remove(enumerator);

            if (removed)
            {
                if (!child.keyValuePair.HasValue && child.children == null)
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

        protected bool Contains(IEnumerator<int> enumerator)
        {
            TrieDictionaryNode<TKey, TValue> node = TryGetNode(enumerator);

            return node != null && node.keyValuePair.HasValue;
        }

        protected bool TryGetValue(IEnumerator<int> enumerator, out TValue value)
        {
            TrieDictionaryNode<TKey, TValue> node = TryGetNode(enumerator);

            if (node != null && node.keyValuePair.HasValue)
            {
                value = node.keyValuePair.Value.Value;

                return true;
            }
            else
            {
                value = default(TValue);

                return false;
            }
        }

        private TrieDictionaryNode<TKey, TValue> TryGetNode(IEnumerator<int> enumerator)
        {
            TrieDictionaryNode<TKey, TValue> node = this;

            while (enumerator.MoveNext())
            {
                if (node.children == null)
                {
                    return null;
                }

                node = node.children[enumerator.Current];

                if (node == null)
                {
                    return null;
                }
            }

            return node;
        }

        public void Clear()
        {
            this.childCount = 0;
            this.children = null;
            this.keyValuePair = null;
        }

        protected IEnumerable<KeyValuePair<TKey, TValue>> GetSubTree(IEnumerator<int> enumerator)
        {
            TrieDictionaryNode<TKey, TValue> node = this.TryGetNode(enumerator);

            if (node != null)
            {
                return node;
            }
            else
            {
                return Enumerable.Empty<KeyValuePair<TKey, TValue>>();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (this.keyValuePair.HasValue)
            {
                yield return this.keyValuePair.Value;
            }

            if (this.childCount == 0)
            {
                yield break;
            }

            Stack<NodeCurser> nodeCursorStack = new Stack<NodeCurser>();

            nodeCursorStack.Push(new NodeCurser(this));

            while (nodeCursorStack.Count > 0)
            {
                NodeCurser cursor = nodeCursorStack.Peek();
                TrieDictionaryNode<TKey, TValue> node = null;

                if (cursor.Node.children != null)
                {
                    for ( ; cursor.ChildIndex < cursor.Node.children.Length && node == null ; cursor.ChildIndex++)
                    {
                        node = cursor.Node.children[cursor.ChildIndex];
                    }
                }

                if (node != null)
                {
                    nodeCursorStack.Push(new NodeCurser(node));

                    if (node.keyValuePair.HasValue)
                    {
                        yield return node.keyValuePair.Value;
                    }
                }
                else
                {
                    nodeCursorStack.Pop();
                }
            }
        }

        private class NodeCurser
        {
            public TrieDictionaryNode<TKey, TValue> Node;
            public int ChildIndex;

            public NodeCurser(TrieDictionaryNode<TKey, TValue> node)
            {
                this.Node = node;
                this.ChildIndex = 0;
            }
        }
    }
}
