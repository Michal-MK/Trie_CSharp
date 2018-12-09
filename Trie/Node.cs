using System;

namespace Igor.Trie {

	/// <summary>
	/// Class representing a logical node in a <see cref="Trie{T}"/>
	/// </summary>
	/// <typeparam name="T">Value type of the node and the parent Trie</typeparam>
	public class Node<T> {

		private IAlphabet alphabet;
		private T _value;
		internal string _fullPath;

		/// <summary>
		/// Children of this node
		/// </summary>
		public Node<T>[] children;

		/// <summary>
		/// Value this node is storing
		/// </summary>
		public T Value { get { return _value; } set { _value = value; HasValue = true; } }

		/// <summary>
		/// Is this node holding a value ?
		/// </summary>
		public bool HasValue { get; internal set; } = false;

		/// <summary>
		/// The full key of this <see cref="Trie{T}"/> entry
		/// </summary>
		public string FullKeyPath => _fullPath;

		/// <summary>
		/// Parent node reference
		/// </summary>
		public Node<T> Parent { get; set; }

		/// <summary>
		/// Recursively search the children of this <see cref="Node{T}"/> to find the key
		/// <para>Returns 'null' if key does not have a value</para>
		/// </summary>
		internal Node<T> Search(string key) {
			if(key.Length == 1) {
				return children[alphabet.ToIndex(key[0])];
			}
			if(children[alphabet.ToIndex(key[0])] != null) {
				return children[alphabet.ToIndex(key[0])].Search(key.Substring(1));
			}
			return null;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="alphabet">Reference to the Trie's alphabet</param>
		public Node(IAlphabet alphabet) {
			this.alphabet = alphabet;
			children = new Node<T>[alphabet.letters];
		}

		internal bool HasChildren() {
			foreach (Node<T> node in children) {
				if(node != null) {
					return true;
				}
			}
			return false;
		}

		internal Node<T> Find(string key) {
			if(key.Length > 1) {
				return GetChild(key[0])?.Find(key.Substring(1));
			}
			else {
				return GetChild(key[0]);
			}
		}

		/// <summary>
		/// Remove the value in this <see cref="Node{T}"/>, does nothing if the <see cref="Node{T}"/> has no value
		/// </summary>
		public void RemoveValue() {
			if (!HasValue) { return; }
			HasValue = false;
			Value = default(T);
			RemovePath();
		}

		internal void RemovePath() {
			if (Parent != null) {
				if (!HasChildren()) {
					Parent.GetChild(FullKeyPath[FullKeyPath.Length - 1]) = null;
					Parent.RemovePath();
				}
			}
		}

		/// <summary>
		/// Get the child under selected <see cref="char"/> 'c', null if no child exists
		/// </summary>
		public ref Node<T> GetChild(char c) {
			return ref children[alphabet.ToIndex(c)];
		}

		internal void GetListEntry(System.Collections.Generic.List<(string key, T value)> values) {
			for (int i = 0; i < children.Length; i++) {
				if(children[i] != null) {
					children[i].GetListEntry(values);
				}
			}
			if (HasValue) {
				values.Add((FullKeyPath, Value));
			}
		}

		/// <summary>
		/// Add a child recursively, the value of <see cref="string"/> 's' is being trimmed every layer.
		/// <para>Return true on successful insert</para>
		/// </summary>
		internal bool AddChlid(string s, T value) {
			int index = alphabet.ToIndex(s[0]);

			if (s.Length == 1) {
				if(children[index] != null) {
					return false;
				}
				children[index] = new Node<T>(alphabet) {
					Parent = this,
					Value = value,
					_fullPath = FullKeyPath + s[0]
				};
				return true;
			}
			else {
				if (GetChild(s[0]) == null) {
					children[index] = new Node<T>(alphabet) {
						Parent = this,
						Value = default(T),
						_fullPath = FullKeyPath + s[0]
					};
				}
				return GetChild(s[0]).AddChlid(s.Substring(1), value);
			}
		}
	}
}
