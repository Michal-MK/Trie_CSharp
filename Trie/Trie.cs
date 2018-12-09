using System;
using System.Collections.Generic;

namespace Igor.Trie {
	/// <summary>
	/// <see cref="Trie{T}"/> data structure
	/// </summary>
	/// <typeparam name="T">The type this Trie is holding</typeparam>
	public class Trie<T> {

		/// <summary>
		/// The root of this <see cref="Trie{T}"/>. Can not hold a value! Never <see langword="null"/> !
		/// </summary>
		public Node<T> Root { get; set; }

		/// <summary>
		/// The <see cref="IAlphabet"/> this <see cref="Trie{T}"/> is using for indexing.
		/// </summary>
		public IAlphabet alphabet { get; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public Trie(IAlphabet alphabet) {
			this.alphabet = alphabet;
			Root = new Node<T>(alphabet) {
				HasValue = false,
				_fullPath = ""
			};
		}

		/// <summary>
		/// Is the <see cref="Trie{T}"/> empty?
		/// </summary>
		public bool Empty() {
			return !Root.HasChildren();
		}

		/// <summary>
		/// Inserts a new element into the Trie
		/// <para>Returns a <see cref="bool"/> on insert completed successfully </para>
		/// </summary>
		/// <param name="key">The string under which the data is stored</param>
		/// <param name="value">The value to store</param>
		public bool Insert(string key, T value) {
			if (string.IsNullOrWhiteSpace(key)) {
				return false;
			}
			return Root.AddChlid(key, value);
		}

		/// <summary>
		/// Find the node with the specified key
		/// </summary>
		public Node<T> Search(string key) {
			return Root.Search(key);
		}

		/// <summary>
		/// Remove the <see cref="Node{T}"/> at the index
		/// </summary>
		public void Remove(string key) {
			Node<T> result = Root.Find(key);
			if (result == null) {
				return;
			}
			result.RemoveValue();
		}

		/// <summary>
		/// Get the <see cref="Node{T}"/> by key (<see cref="string"/>) index
		/// </summary>
		/// <param name="key"></param>
		public T this[string key] {
			get {
				Node<T> node = Search(key);
				if (node == null) {
					return default(T);
				}
				return node.Value;
			}
			set {
				Insert(key, value);
			}
		}

		/// <summary>
		/// Returns a .dot file format structured <see cref="string"/>
		/// </summary>
		public string Draw() {
			throw new NotImplementedException("This functionality is not implemented yet");
		}

		/// <summary>
		/// Returns the values in the form of a <see cref="List{T}"/>
		/// </summary>
		public List<(string key, T value)> Items() {
			List<(string key, T value)> values = new List<(string key, T value)>();
			Root.GetListEntry(values);
			return values;
		}
	}
}
