using System;
using System.Diagnostics;
using System.Globalization;

namespace WilsonORWrapper.Logging
{
	/// <summary>
	/// A logging client using <see cref="System.Diagnostics.EventLog"/>.
	/// All messages are logged to the Application log. The source name of all events
	/// is the same as <see cref="ConfigurationSettings.ConnectionString"/>.
	/// </summary>
	public class EventLogger : ILogger, IDisposable
	{
		private const string LOG_NAME = "Application";
		private const string MACHINE_NAME = ".";

		private EventLog _eventLog;

		#region Constructors
		/// <summary>
		/// Initializes the logger by creating the necessary
		/// <see cref="EventLog"/> source.
		/// </summary>
		public EventLogger()
		{
			string source = ConfigurationSettings.Settings.ConnectionString;

			// Create the source, if it does not already exist.
			if (!EventLog.SourceExists(source, MACHINE_NAME))
			{
				EventSourceCreationData sourceData = new EventSourceCreationData(source, LOG_NAME);
				EventLog.CreateEventSource(sourceData);
			}

			_eventLog = new EventLog(LOG_NAME, MACHINE_NAME, source);
		}
		#endregion

		#region ILogger Members
		/// <summary>
		/// Debug messages are not supported by the EventLogger, so this does nothing.
		/// </summary>
		/// <param name="message">Ignored.</param>
		public void Debug(string message)
		{
			//do nothing
		}
		/// <summary>
		/// Debug messages are not supported by the EventLogger, so this does nothing.
		/// </summary>
		/// <param name="message">Ignored.</param>
		/// <param name="args">Ignored.</param>
		public void Debug(string message, params object[] args)
		{
			//do nothing
		}
		/// <summary>
		/// Debug messages are not supported by the EventLogger, so this always returns false.
		/// </summary>
		public bool IsDebugEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Logs an Information message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Info(string message)
		{
			_eventLog.WriteEntry(message, EventLogEntryType.Information);
		}
		/// <summary>
		/// Logs an Information message.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Info(string message, params object[] args)
		{
			_eventLog.WriteEntry(String.Format(CultureInfo.CurrentCulture, message, args), EventLogEntryType.Information);
		}
		/// <summary>
		/// Info messages are always supported by the EventLogger, so this always returns true.
		/// </summary>
		public bool IsInfoEnabled
		{
			get { return true; }
		}

		/// <summary>
		/// Logs a Warning message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Warning(string message)
		{
			_eventLog.WriteEntry(message, EventLogEntryType.Warning);
		}
		/// <summary>
		/// Logs a Warning message.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Warning(string message, params object[] args)
		{
			_eventLog.WriteEntry(String.Format(CultureInfo.CurrentCulture, message, args), EventLogEntryType.Warning);
		}
		/// <summary>
		/// Warning messages are always supported by the EventLogger, so this always returns true.
		/// </summary>
		public bool IsWarningEnabled
		{
			get { return true; }
		}

		/// <summary>
		/// Logs an Error message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public void Error(string message)
		{
			_eventLog.WriteEntry(message, EventLogEntryType.Error);
		}
		/// <summary>
		/// Logs an Error message.
		/// </summary>
		/// <param name="message">The message (containing format items) to log.</param>
		/// <param name="args">The format arguments.</param>
		public void Error(string message, params object[] args)
		{
			_eventLog.WriteEntry(String.Format(CultureInfo.CurrentCulture, message, args), EventLogEntryType.Error);
		}
		/// <summary>
		/// Error messages are always supported by the EventLogger, so this always returns true.
		/// </summary>
		public bool IsErrorEnabled
		{
			get { return true; }
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Closes the underlying <see cref="EventLog"/>.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true);
		}
		/// <summary>
		/// Closes the underlying <see cref="EventLog"/>.
		/// </summary>
		/// <param name="disposing">Identifies whether the object is being disposed.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_eventLog.Close();
				_eventLog = null;
			}
		}          
		#endregion
	}
}
