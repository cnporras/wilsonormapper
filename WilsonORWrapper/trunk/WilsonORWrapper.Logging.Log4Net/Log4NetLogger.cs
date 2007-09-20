using System;
using log4net;

namespace WilsonORWrapper.Logging.Log4Net
{
	/// <summary>
	/// A logging client using <see cref="log4net"/>.
	/// </summary>
	public class Log4NetLogger : ILogger
	{
		private readonly ILog _log4net;

		#region Constructors
		/// <summary>
		/// Initializes the logger using an instance of <see cref="log4net.ILog"/>.
		/// </summary>
		/// <param name="log">The log to use.</param>
		public Log4NetLogger(ILog log)
		{
			if (log == null)
			{
				throw new ArgumentNullException("log");
			}

			_log4net = log;
		}
		#endregion

		#region ILogger Members
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Debug(string message)
		{
			if (_log4net.IsDebugEnabled)
			{
				_log4net.Debug(message);
			}
		}
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Debug(string message, params object[] args)
		{
			if (_log4net.IsDebugEnabled)
			{
				_log4net.DebugFormat(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the debug level is enabled, false otherwise.
		/// </summary>
		public bool IsDebugEnabled
		{
			get { return _log4net.IsDebugEnabled; }
		}

		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Info(string message)
		{
			if (_log4net.IsInfoEnabled)
			{
				_log4net.Info(message);
			}
		}
		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Info(string message, params object[] args)
		{
			if (_log4net.IsInfoEnabled)
			{
				_log4net.InfoFormat(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the info level is enabled, false otherwise.
		/// </summary>
		public bool IsInfoEnabled
		{
			get { return _log4net.IsInfoEnabled; }
		}

		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Warning(string message)
		{
			if (_log4net.IsWarnEnabled)
			{
				_log4net.Warn(message);
			}
		}
		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Warning(string message, params object[] args)
		{
			if (_log4net.IsWarnEnabled)
			{
				_log4net.WarnFormat(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the warning level is enabled, false otherwise.
		/// </summary>
		public bool IsWarningEnabled
		{
			get { return _log4net.IsWarnEnabled; }
		}

		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Error(string message)
		{
			if (_log4net.IsErrorEnabled)
			{
				_log4net.Error(message);
			}
		}
		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Error(string message, params object[] args)
		{
			if (_log4net.IsErrorEnabled)
			{
				_log4net.ErrorFormat(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the error level is enabled, false otherwise.
		/// </summary>
		public bool IsErrorEnabled
		{
			get { return _log4net.IsErrorEnabled; }
		}
		#endregion
	}
}
