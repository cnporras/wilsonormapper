using System;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the interface for a factory class that creates an instance of
	/// <see cref="ILogger"/>.
	/// </summary>
	public interface ILoggerFactory
	{
		/// <summary>
		/// A method which creates and returns an implementation of <see cref="ILogger"/>.
		/// </summary>
		/// <returns>The created object.</returns>
		ILogger CreateLogger();
	}
}
