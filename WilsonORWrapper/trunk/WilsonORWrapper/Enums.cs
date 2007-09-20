using System;
using System.Collections.Generic;
using System.Text;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the source of a mappings file.
	/// </summary>
	public enum MappingsFileSource
	{
		/// <summary>
		/// Mappings file exists as a physical file on the file system.
		/// </summary>
		FileSystem,
		/// <summary>
		/// Mappings file exists as an embedded resource in an assembly.
		/// </summary>
		Assembly
	}

	/// <summary>
	/// The persistence state of an entity.
	/// </summary>
	public enum EntityState
	{
		/// <summary>
		/// New
		/// </summary>
		New = 0,
		/// <summary>
		/// Existing not Modified
		/// </summary>
		Unchanged = 1,
		/// <summary>
		/// Existing and Modified
		/// </summary>
		Changed = 2,
		/// <summary>
		/// Deleted
		/// </summary>
		Deleted = 3,
		/// <summary>
		/// Unknown -- typically when item is not tracked by mapper
		/// </summary>
		Unknown = 4
	}

	/// <summary>
	/// Reflects which events should be logged.
	/// </summary>
	public enum LoggingLevel
	{
		/// <summary>
		/// No logging
		/// </summary>
		None = 0,
		/// <summary>
		/// Debug logging level -- intercepts O/R mapper commands
		/// </summary>
		Debug = 1
	}

	/// <summary>
	/// An ILogger implementation type.
	/// </summary>
	public enum LoggerType
	{
		/// <summary>
		/// No logger
		/// </summary>
		None = 0,
		/// <summary>
		/// Windows event log logger
		/// </summary>
		EventLog,
		/// <summary>
		/// Log4Net logger
		/// </summary>
		Log4Net,
		/// <summary>
		/// NLog logger
		/// </summary>
		NLog
	}

	/// <summary>
	/// An ICache implementation type.
	/// </summary>
	public enum CacheType
	{
		/// <summary>
		/// No cache
		/// </summary>
		None = 0,
		/// <summary>
		/// Asp.Net caching
		/// </summary>
		AspNet = 1,
		/// <summary>
		/// Memcached caching
		/// </summary>
		Memcached = 2
	}
}
