using System;

namespace WilsonORWrapper.Logging
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="EventLogger"/>.
	/// </summary>
	public class EventLoggerFactory : ILoggerFactory
	{
		#region ILoggerFactory Members
		/// <summary>
		/// Creates and returns an instance of <see cref="EventLogger"/>.
		/// </summary>
		/// <returns>An <see cref="EventLogger"/> object.</returns>
		public ILogger CreateLogger()
		{
			ILogger logger = new EventLogger();

			return logger;
		}
		#endregion
	}
}
