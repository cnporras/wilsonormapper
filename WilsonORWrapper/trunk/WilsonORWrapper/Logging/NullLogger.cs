using System;

namespace WilsonORWrapper.Logging
{
	/// <summary>
	/// Singleton instance of a logger that does nothing.
	/// </summary>
	public class NullLogger : ILogger
	{
		private static readonly NullLogger _instance = new NullLogger();

		/// <summary>
		/// The singleton instance for the logger.
		/// </summary>
		public static NullLogger Instance
		{
			get { return _instance; }
		}

		#region Constructors
		/// <summary>
		/// Initializes the logger.
		/// </summary>
		protected NullLogger()
		{
		}
		#endregion

		#region	ILogger Members
		#region Debug
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		public void Debug(string message)
		{
			// do nothing
		}
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		/// <param name="args">Doesn't matter.</param>
		public void Debug(string message, params object[] args)
		{
			// do nothing
		}
		/// <summary>
		/// Always returns false.
		/// </summary>
		public bool IsDebugEnabled
		{
			get { return false; }
		}
		#endregion

		#region Info
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		public void Info(string message)
		{
			// do nothing
		}
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		/// <param name="args">Doesn't matter.</param>
		public void Info(string message, params object[] args)
		{
			// do nothing
		}
		/// <summary>
		/// Always returns false.
		/// </summary>
		public bool IsInfoEnabled
		{
			get { return false; }
		}
		#endregion

		#region Warning
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		public void Warning(string message)
		{
			// do nothing
		}
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		/// <param name="args">Doesn't matter.</param>
		public void Warning(string message, params object[] args)
		{
			// do nothing
		}
		/// <summary>
		/// Always returns false.
		/// </summary>
		public bool IsWarningEnabled
		{
			get { return false; }
		}
		#endregion

		#region Error
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		public void Error(string message)
		{
			// do nothing
		}
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="message">Doesn't matter.</param>
		/// <param name="args">Doesn't matter.</param>
		public void Error(string message, params object[] args)
		{
			// do nothing
		}
		/// <summary>
		/// Always returns false.
		/// </summary>
		public bool IsErrorEnabled
		{
			get { return false; }
		}
		#endregion

		/// <summary>
		/// Always returns <c>LoggingLevel.None</c>.
		/// </summary>
		public static LoggingLevel LoggingLevel
		{
			get { return LoggingLevel.None; }
		}
		#endregion
	}
}
