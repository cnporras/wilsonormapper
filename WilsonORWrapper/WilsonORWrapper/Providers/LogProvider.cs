using System;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace WilsonORWrapper.Providers
{
	/// <summary>
	/// Singleton class which exposes internal classes and methods for interacting
	/// with the logging interface.
	/// </summary>
	public static class LogProvider
	{
		private static volatile ILogger _logger;
		private static readonly Object _loggerLock = new Object();

		#region Properties
		/// <summary>
		/// Returns an <see cref="ILogger"/> instance based on the configuration properties.
		/// This always returns a non-null logger, and will always return the same instance
		/// of the same logger.
		/// </summary>
		public static ILogger Logger
		{
			get
			{
				if (_logger == null)
				{
					lock (_loggerLock)
					{
						if (_logger == null)
						{
							_logger = GetLogger();
						}
					}
				}

				return _logger;
			}
		}
		#endregion

		#region Private Methods
		private static ILogger GetLogger()
		{
			LoggerType loggerType = ConfigurationSettings.Settings.Logger;
			string assemblyName;
			string factoryName;

			switch (loggerType)
			{
				case LoggerType.None:
					assemblyName = "WilsonORWrapper.dll";
					factoryName = "WilsonORWrapper.Logging.NullLoggerFactory";
					break;
				case LoggerType.EventLog:
					assemblyName = "WilsonORWrapper.dll";
					factoryName = "WilsonORWrapper.Logging.EventLoggerFactory";
					break;
				case LoggerType.Log4Net:
					assemblyName = "WilsonORWrapper.Logging.Log4Net.dll";
					factoryName = "WilsonORWrapper.Logging.Log4Net.Log4NetFactory";
					break;
				case LoggerType.NLog:
					assemblyName = "WilsonORWrapper.Logging.NLog.dll";
					factoryName = "WilsonORWrapper.Logging.NLog.NLogFactory";
					break;
				default:
					throw new ConfigurationErrorsException("Specified Logger type in configuration file is unknown or invalid.");
			}

			if (System.Web.HttpContext.Current != null)
			{
				assemblyName = HttpRuntime.BinDirectory + assemblyName;
			}

			Assembly assembly = Assembly.LoadFrom(assemblyName);
			if (assembly == null)
				throw new LogProviderException(assemblyName);

			ILoggerFactory factory = assembly.CreateInstance(factoryName) as ILoggerFactory;
			if (factory == null)
				throw new LogProviderException(factoryName);

			ILogger logger = factory.CreateLogger();
			if (logger == null)
				throw new LogProviderException("Unable to create logger");

			return logger;
		}
		#endregion
	}
}
