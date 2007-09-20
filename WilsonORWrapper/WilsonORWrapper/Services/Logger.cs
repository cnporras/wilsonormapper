using System;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Services
{
	/// <summary>
	/// Singleton class which provides logging functionality.
	/// </summary>
	public static class Logger
	{
		private static readonly ILogger _logger = LogProvider.Logger;

		#region Debug
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void Debug(string message)
		{
			_logger.Debug(message);
		}
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public static void Debug(string message, params object[] args)
		{
			_logger.Debug(message, args);
		}
		/// <summary>
		/// Returns true if logging at the debug level is enabled, false otherwise.
		/// </summary>
		public static bool IsDebugEnabled
		{
			get { return _logger.IsDebugEnabled; }
		}
		#endregion

		#region Info
		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void Info(String message)
		{
			_logger.Info(message);
		}
		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public static void Info(String message, params object[] args)
		{
			_logger.Info(message, args);
		}
		/// <summary>
		/// Returns true if logging at the info level is enabled, false otherwise.
		/// </summary>
		public static bool IsInfoEnabled
		{
			get { return _logger.IsInfoEnabled; }
		}
		#endregion

		#region Warning
		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void Warning(String message)
		{
			_logger.Warning(message);
		}
		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public static void Warning(String message, params object[] args)
		{
			_logger.Warning(message, args);
		}
		/// <summary>
		/// Returns true if logging at the warning level is enabled, false otherwise.
		/// </summary>
		public static bool IsWarningEnabled
		{
			get { return _logger.IsWarningEnabled; }
		}
		#endregion

		#region Error
		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void Error(String message)
		{
			_logger.Error(message);
		}
		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public static void Error(String message, params object[] args)
		{
			_logger.Error(message, args);
		}
		/// <summary>
		/// Returns true if logging at the error level is enabled, false otherwise.
		/// </summary>
		public static bool IsErrorEnabled
		{
			get { return _logger.IsErrorEnabled; }
		}
		#endregion
	}
}
