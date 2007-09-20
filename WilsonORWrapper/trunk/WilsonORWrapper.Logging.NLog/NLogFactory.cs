using System;
using NLog;

namespace WilsonORWrapper.Logging.NLog
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="NLogLogger"/>.
	/// </summary>
	public class NLogFactory : ILoggerFactory
	{
		#region ILoggerFactory Members
		/// <summary>
		/// Creates and returns an instance of <see cref="NLogLogger"/>.
		/// </summary>
		/// <returns>An <see cref="NLogLogger"/> object.</returns>
		public ILogger CreateLogger()
		{
			Logger nlog = LogManager.GetLogger("WilsonORWrapper");
			ILogger logger = new NLogLogger(nlog);

			return logger;
		}
		#endregion
	}
}
