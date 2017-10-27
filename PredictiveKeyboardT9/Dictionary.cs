using System.Collections.Generic;

namespace PredictiveKeyboardT9
{
    public class Dictionary
    {
        public readonly Node Root = new Node();

        public Dictionary(IEnumerable<string> words)
        {
            foreach (var w in words)
            {
                Add(w);
            }
        }

        private void Add(string s)
        {
            Add(s, Root);
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

        private static bool IsEndOfWord(string s)
        {
            return s.Length == 1;
        }

        public class Node
        {
            public readonly Dictionary<char, Node> Children = new Dictionary<char, Node>();

            private bool IsCompleteWord { get; }
            private int WordsCount { get; set; }

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

            private Node GetChild(char c)
            {
                return Children[c];
            }

            public IEnumerable<string> GetCompleteWords(string prefix)
            {
                return GetCompleteWords(prefix, this, new List<string>());
            }

            private static IEnumerable<string> GetCompleteWords(string prefix, Node node, ICollection<string> words)
            {
                if (node.IsCompleteWord) words.Add(prefix);
                foreach (var nodeChild in node.Children)
                {
                    GetCompleteWords(prefix + nodeChild.Key, nodeChild.Value, words);
                }
                return words;
            }
        }
    }
}