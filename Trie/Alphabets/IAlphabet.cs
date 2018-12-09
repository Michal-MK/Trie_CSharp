namespace Igor.Trie {

	/// <summary>
	/// Base Interface for all <see cref="Trie{T}"/>'s alphabets
	/// </summary>
	public interface IAlphabet {
		/// <summary>
		/// The characters of this alphabet
		/// </summary>
		char[] alphabet { get; }

		/// <summary>
		/// The number of letters in this alphabet
		/// </summary>
		int letters { get; }

		/// <summary>
		/// Convert the character to index into the <see cref="Trie{T}"/>'s children container
		/// </summary>
		int ToIndex(char c);

		/// <summary>
		/// Convert the index to a <see cref="char"/> corresponding to the position in <see cref="IAlphabet"/>'s alphabet array
		/// </summary>
		char ToChar(int index);
	}
}
