using System;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines classes that can be identified by an <see cref="IIdentity" />.
	/// </summary>
	public interface IIdentifiable
	{
		/// <summary>
		/// Returns the <see cref="IIdentity" /> of the object.
		/// </summary>
		/// <returns>The identity object.</returns>
		IIdentity GetIdentity();
	}
}
