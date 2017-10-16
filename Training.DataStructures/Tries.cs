using System.Collections.Generic;
using Training.Common;

namespace Training.DataStructures
{
    public class Tries
    {
        private readonly Node _root = new Node();

        public void Add(string s)
        {
            Add(s, _root);
        }

        private static void Add(string s, Node node)
        {
            while (s != string.Empty)
            {
                var nextNode = node.Add(s[0], IsEndOfWord(s));
                s = s.Substring(1);
                node = nextNode;
            }
        }

        public void Print(IPrint printer)
        {
            PrintChildren(_root, string.Empty, printer);
        }

        private static void PrintChildren(Node node, string prefix, IPrint printer)
        {
            foreach (var kv in node.Children)
            {
                if (kv.Value.IsCompleteWord) printer.Print(prefix + kv.Key);
                PrintChildren(kv.Value, prefix + kv.Key, printer);
            }
        }

        private static bool IsEndOfWord(string s)
        {
            return s.Length == 1;
        }

        public int Find(string s)
        {
            return Find(s, _root);
        }

        private static int Find(string s, Node node)
        {
            var prefixComplet = true;
            while (s != string.Empty)
            {
                prefixComplet = prefixComplet && node.Contains(s[0]);
                if (!node.Contains(s[0])) break;
                node = node.GetChild(s[0]);
                s = s.Substring(1);
            }
            return !prefixComplet ? 0 : node.WordsCount;
        }

        private class Node
        {
            public readonly Dictionary<char, Node> Children = new Dictionary<char, Node>();
            public bool IsCompleteWord { get; }
            public int WordsCount { get; private set; }

            public Node() : this(false)
            {
            }

            private Node(bool isCompleteWord)
            {
                IsCompleteWord = isCompleteWord;
            }

            public Node Add(char c, bool isCompleteWord)
            {
                if (!Contains(c))
                {
                    Children.Add(c, new Node(isCompleteWord));
                }
                var node = GetChild(c);
                node.WordsCount++;
                return node;
            }

            public bool Contains(char c)
            {
                return Children.ContainsKey(c);
            }

            public Node GetChild(char c)
            {
                return Children[c];
            }
        }
    }
}