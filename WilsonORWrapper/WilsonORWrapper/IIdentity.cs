using System;
using System.Collections;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the keys and key/value pairs of a table's 
	/// primary key identity.
	/// </summary>
	public interface IIdentity : IComparable
	{
		/// <summary>
		/// The key names, as an array of strings. This should be
		/// implemented as a read-only property.
		/// </summary>
		string[] Keys { get; }
		/// <summary>
		/// Returns a list of key/value pairs reflecting the
		/// key values for this instance.
		/// </summary>
		/// <returns></returns>
		DictionaryEntry[] GetIdentityEntries();
	}
}
