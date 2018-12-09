namespace Igor.Trie {
	
	/// <summary>
	/// Alphabet containing only letters 'a-z' 
	/// </summary>
	public class LowercaseLetters : IAlphabet {

		/// <summary>
		/// The alphabet
		/// </summary>
		public char[] alphabet => new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
										  'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
										  'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
										  'y', 'z', };

		/// <summary>
		/// Number of characters in this <see cref="IAlphabet"/>
		/// </summary>
		public int letters => alphabet.Length;

		/// <summary>
		/// Convert the character to index into the <see cref="Trie{T}"/>'s children container
		/// </summary>
		public char ToChar(int index) {
			return alphabet[index];
		}

		/// <summary>
		/// Convert the index to a <see cref="char"/> corresponding to the position in <see cref="IAlphabet"/>'s alphabet array
		/// </summary>
		public int ToIndex(char c) {
			return c - 'a';
		}
	}
}
