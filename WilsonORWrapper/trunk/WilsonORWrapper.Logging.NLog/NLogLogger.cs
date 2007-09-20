using System;
using NLog;

namespace WilsonORWrapper.Logging.NLog
{
	/// <summary>
	/// A logging client using <see cref="NLog"/>.
	/// </summary>
	public class NLogLogger : ILogger
	{
		private readonly Logger _nlog;

		#region Constructors
		/// <summary>
		/// Initializes the logger using an instance of <see cref="Logger"/>.
		/// </summary>
		/// <param name="log">The log to use.</param>
		public NLogLogger(Logger log)
		{
			if (log == null)
			{
				throw new ArgumentNullException("log");
			}

			_nlog = log;
		}
		#endregion

		#region ILogger Members
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Debug(string message)
		{
			if ( _nlog.IsDebugEnabled)
			{
				_nlog.Debug(message);
			}
		}
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Debug(string message, params object[] args)
		{
			if (_nlog.IsDebugEnabled)
			{
				_nlog.Debug(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the debug level is enabled, false otherwise.
		/// </summary>
		public bool IsDebugEnabled
		{
			get { return _nlog.IsDebugEnabled; }
		}

		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Info(string message)
		{
			if ( _nlog.IsInfoEnabled)
			{
				_nlog.Info(message);
			}
		}
		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Info(string message, params object[] args)
		{
			if (_nlog.IsInfoEnabled)
			{
				_nlog.Info(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the info level is enabled, false otherwise.
		/// </summary>
		public bool IsInfoEnabled
		{
			get { return _nlog.IsInfoEnabled; }
		}

		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Warning(string message)
		{
			if ( _nlog.IsWarnEnabled)
			{
				_nlog.Warn(message);
			}
		}
		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Warning(string message, params object[] args)
		{
			if (_nlog.IsWarnEnabled)
			{
				_nlog.Warn(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the warning level is enabled, false otherwise.
		/// </summary>
		public bool IsWarningEnabled
		{
			get { return _nlog.IsWarnEnabled; }
		}

		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Error(string message)
		{
			if (_nlog.IsErrorEnabled)
			{
				_nlog.Error(message);
			}
		}
		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Error(string message, params object[] args)
		{
			if (_nlog.IsErrorEnabled)
			{
				_nlog.Error(message, args);
			}
		}
		/// <summary>
		/// Returns true if logging at the error level is enabled, false otherwise.
		/// </summary>
		public bool IsErrorEnabled
		{
			get { return _nlog.IsErrorEnabled; }
		}
		#endregion
	}
}
