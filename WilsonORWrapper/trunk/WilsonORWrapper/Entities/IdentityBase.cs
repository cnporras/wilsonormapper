using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace WilsonORWrapper.Entities
{
	/// <summary>
	/// Base class for all identity objects.
	/// </summary>
	public abstract class IdentityBase : IIdentity, IComparable
	{
		#region IIdentity Members
		/// <summary>
		/// Abstract property which returns an array of <see cref="string"/>s
		/// which reflect the primary key field names.
		/// </summary>
		public abstract string[] Keys
		{
			get; 
		}
		/// <summary>
		/// Abstract method which child classes must override to return an 
		/// array of <see cref="DictionaryEntry"/> objects which reflect the
		/// primary key identity of the object. The key must be the primary key
		/// name, and the value must be the primary key's value.
		/// </summary>
		/// <returns>The identity's key/value pair(s).</returns>
		public abstract DictionaryEntry[] GetIdentityEntries();
		#endregion

		#region IComparable
		/// <summary>
		/// Compares this identity to another identity by comparing each object's properties
		/// as exposed by calling the <see cref="GetIdentityEntries()"/> method.
		/// Note that this does not compare based on reference equality; to check
		/// object reference equality, use the == operator.
		/// </summary>
		/// <param name="obj">The identity to compare to. Must implement <see cref="IIdentity" />.</param>
		/// <returns>A number that reflects the comparative similarity of identities.
		/// A value of "0" indicates equality; 
		/// "1" indicates that obj is null or not of type <see cref="IIdentity"/>;
		/// "-1" indicates that the objects are not equal.
		/// </returns>
		public int CompareTo(object obj)
		{
			if (obj == this)
				return 0;

			IIdentity ident = obj as IIdentity;
			if (ident == null)
				return 1;
			
			DictionaryEntry[] ident1 = this.GetIdentityEntries();
			DictionaryEntry[] ident2 = ident.GetIdentityEntries();
			
			if (ident1 == null || ident2 == null)
				return 1;

			if ( ident1.Length != ident2.Length )
				return -1;

			for (int idx = 0; idx < ident1.Length; idx++)
			{
				if (!ident1[idx].Equals(ident2[idx]))
					return -1;
			}

			return 0;
		}
		#endregion

		#region System.Object
		/// <summary>
		/// Indicates whether this identity is equal to another specified identity by first
		/// checking to make sure the objects are of the same type, then calling
		/// the <see cref="CompareTo"/> method.
		/// Note that this does not check for object reference equality; to check
		/// object reference equality, use the == operator.
		/// </summary>
		/// <returns>True if the objects have equal property values, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj == this)
				return true;

			IIdentity identity = obj as IIdentity;
			if (identity == null)
				return false;

			return (this.CompareTo(identity) == 0);
		}
		/// <summary>
		/// Returns the hash code for this identity. <see cref="EntityBase{T}"/> hash codes 
		/// are equal to <see cref="IdentityBase"/> hash codes.
		/// </summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			DictionaryEntry[] entries = this.GetIdentityEntries();
			if (entries.Length == 0)
				return base.GetHashCode();
			if (entries.Length == 1)
				return entries[0].GetHashCode();

			StringBuilder hashstring = new StringBuilder();
			for (int idx = 0; idx < entries.Length; idx++)
			{
				hashstring.Append(entries[idx].GetHashCode().ToString(CultureInfo.InvariantCulture));
			}

			return hashstring.GetHashCode();
		}
		#endregion
	}
}
