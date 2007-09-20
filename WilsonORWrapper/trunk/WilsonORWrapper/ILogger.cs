using System;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the implementation of a logging client.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Debug(String message);
		/// <summary>
		/// Logs a message at the debug level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		void Debug(String message, params object[] args);
		/// <summary>
		/// Returns true if logging at the debug level is enabled, false otherwise.
		/// </summary>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Info(String message);
		/// <summary>
		/// Logs a message at the info level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		void Info(String message, params object[] args);
		/// <summary>
		/// Returns true if logging at the info level is enabled, false otherwise.
		/// </summary>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Warning(String message);
		/// <summary>
		/// Logs a message at the warning level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		void Warning(String message, params object[] args);
		/// <summary>
		/// Returns true if logging at the warning level is enabled, false otherwise.
		/// </summary>
		bool IsWarningEnabled { get; }

		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Error(String message);
		/// <summary>
		/// Logs a message at the error level.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		void Error(String message, params object[] args);
		/// <summary>
		/// Returns true if logging at the error level is enabled, false otherwise.
		/// </summary>
		bool IsErrorEnabled { get; }
	}
}
