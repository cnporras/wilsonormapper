using System;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;

namespace WilsonORWrapper.Logging.Log4Net
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="Log4NetLogger"/>.
	/// </summary>
	public class Log4NetFactory : ILoggerFactory
	{
		#region ILoggerFactory Members
		/// <summary>
		/// Creates and returns an instance of <see cref="Log4NetLogger"/>.
		/// </summary>
		/// <returns>An <see cref="Log4NetLogger"/> object.</returns>
		public ILogger CreateLogger()
		{
			string configFile = ConfigurationManager.AppSettings["log4net-config-file"];

			if (String.IsNullOrEmpty(configFile))
			{
				//configure based on application config file
				XmlConfigurator.Configure();
			}
			else
			{
				//config based on file
				XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
			}

			ILog log4net = LogManager.GetLogger("WilsonORWrapper");
			ILogger logger = new Log4NetLogger(log4net);

			return logger;
		}
		#endregion
	}
}
