using System;

namespace WilsonORWrapper.Logging
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="NullLogger"/>.
	/// </summary>
	public class NullLoggerFactory : ILoggerFactory
	{
		#region ILoggerFactory Members
		/// <summary>
		/// Creates and returns an instance of <see cref="NullLogger"/>.
		/// </summary>
		/// <returns>A <see cref="NullLogger"/> object.</returns>
		public ILogger CreateLogger()
		{
			ILogger logger = NullLogger.Instance;

			return logger;
		}
		#endregion
	}
}
