using System;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the interface for a factory class that creates an instance of
	/// <see cref="ICache"/>.
	/// </summary>
	public interface ICacheFactory
	{
		/// <summary>
		/// A method which creates and returns an implementation of <see cref="ICache"/>.
		/// </summary>
		/// <returns>The created object.</returns>
		ICache CreateCache();
	}
}
