using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igor.Trie;

namespace TrieTests {
	[TestClass]
	public class TrieTests {

		[TestMethod]
		public void CreateTrie() {
			Trie<int> t = new Trie<int>(new LowercaseLetters());
			Assert.IsTrue(t.Root != null);
			Assert.IsTrue(t.Root.HasValue == false);
			Assert.IsTrue(t.Root.Parent == null);
			Assert.IsTrue(t.Items().Count == 0);
			Assert.IsTrue(t.Empty());
			Assert.IsTrue(t.Root.FullKeyPath == "");
			Assert.IsTrue(t.Root.children.Length == t.alphabet.letters);
		}

		[TestMethod]
		public void Trie_Insert() {
			Trie<int> t = new Trie<int>(new LowercaseLetters());

			Assert.IsTrue(t.Insert("a", 1));
			Assert.IsTrue(t.Insert("ab", 2));

			Assert.ThrowsException<IndexOutOfRangeException>(() => t.Insert("Abc", 1));
			Assert.IsFalse(t.Insert("a", 1));
			Assert.IsFalse(t.Insert("", 1));

			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].FullKeyPath == "a");
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].Value == 1);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].Parent == t.Root);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].HasValue);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children.Length == t.alphabet.letters);

			Node<int> nodeP = t.Root.children[t.alphabet.ToIndex('a')];

			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children[t.alphabet.ToIndex('b')].FullKeyPath == "ab");
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children[t.alphabet.ToIndex('b')].Value == 2);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children[t.alphabet.ToIndex('b')].Parent == nodeP);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children[t.alphabet.ToIndex('b')].HasValue);
			Assert.IsTrue(t.Root.children[t.alphabet.ToIndex('a')].children[t.alphabet.ToIndex('b')].children.Length == t.alphabet.letters);
		}

		[TestMethod]
		public void Trie_Search() {
			Trie<int> t = new Trie<int>(new LowercaseLetters());

			t.Insert("a",20);
			t.Insert("b",20);
			t.Insert("c",20);
			t.Insert("ab",20);

			Assert.IsNull(t.Search("d"));
			Assert.IsTrue(t.Search("a") != null);
			Assert.IsTrue(t.Search("a").HasValue);
			Assert.IsTrue(t.Search("a").FullKeyPath == "a");
			Assert.IsTrue(t.Search("a").Parent == t.Root);
			Assert.IsTrue(t.Search("a").Value == 20);

			Assert.IsTrue(t.Search("c") != null);
			Assert.IsTrue(t.Search("ab") != null);
		}

		[TestMethod]
		public void Trie_Remove() {
			Trie<int> t = new Trie<int>(new LowercaseLetters());
			t.Insert("a", 50);
			t.Remove("a");
			t.Remove("asocer"); // Does nothing

			Assert.IsTrue(t.Root.GetChild('a') == null);
			Assert.IsTrue(t.Search("a") == null);
			Assert.IsTrue(t.Empty());

			t.Insert("abcde", 20);
			t.Remove("abcde");
			Assert.IsTrue(t.Empty());
		}

		[TestMethod]
		public void Trie_Indexer() {
			Trie<int> t = new Trie<int>(new LowercaseLetters());
			t.Insert("a", 50);

			Assert.IsTrue(t.Search("a").Value == t["a"]);
			Assert.IsTrue(t.Search("a").Value != t["b"]);
			Assert.IsTrue(t.Search("a").Value != t["b"]);

			t["ahoj"] = 50;

			Assert.IsTrue(t.Search("ahoj") != null);
			Assert.IsTrue(t.Search("ahoj").Value == 50);
			Assert.IsTrue(t["ahoj"] == 50);
		}
	}
}


/*
 			{
				Trie<int> t = new Trie<int>(new LowercaseLetters());
				t.Insert("aaaaa", 400);

				Assert.IsTrue(t.Search("aaaaa") != null);
				t.Remove("aaaaa");
				Assert.IsTrue(t.Search("aaaaa") == null);
			}

			{
				Trie<int> t = new Trie<int>(new LowercaseLetters());
				t.Insert("a", 200);

				Assert.IsTrue(t.Root.Parent == null);
				Assert.IsTrue(t.Search("a").Parent == t.Root);

			}

	 */
