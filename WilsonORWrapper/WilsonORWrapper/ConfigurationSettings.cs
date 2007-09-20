using System;
using System.Configuration;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper
{
	/// <summary>
	/// Custom configuration section handler for the service layer settings.
	/// Used techniques documented at http://www.haacked.com/archive/2007/03/12/custom-configuration-sections-in-3-easy-steps.aspx.
	/// </summary>
	/// <example>
	/// To use, add the following to your application configuration file.
	/// 
	/// <code><![CDATA[
	/// <configuration>
	/// 	<configSections>
	/// 		<section name="wilsonorwrapper" type="WilsonORWrapper.ConfigurationSettings,WilsonORWrapper" />
	/// 	</configSections>
	/// 	<connectionStrings>
	/// 		<add name="MyDatabase" 
	/// 			connectionString="Data Source=(local);Initial Catalog=DATABASE;Integrated Security=True"
	/// 			providerName="System.Data.SqlClient" />
	/// 	</connectionStrings>
	/// 	<wilsonorwrapper
	///			  mappingsFileName="Mappings.config" 
	/// 		  mappingsFileLocation="MyApp.dll" 
	/// 		  mappingsFileSource="Assembly" 
	/// 		  connectionString="MyDatabase" 
	/// 		  logger="NLog" 
	///			  cache="AspNet" 
	///		/>
	/// </configuration>
	/// ]]></code>
	/// </example>
	public class ConfigurationSettings : ConfigurationSection
	{
		private static volatile ConfigurationSettings _settings;
		private static readonly Object _settingsLock = new Object();

		#region Constructors
		/// <summary>
		/// Protected constructor to enforce singleton.
		/// </summary>
		protected ConfigurationSettings()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns an instance of the Service Layer's settings as derived from the config file.
		/// If initialization has not been done yet, calling this will first call the 
		/// <see cref="ConfigurationSettings.Initialize()"/> method.
		/// </summary>
		public static ConfigurationSettings Settings
		{
			get
			{
				if (_settings == null)
				{
					Initialize();
				}
				return _settings;
			}
		}

		/// <summary>
		/// The name of the mappings XML file. The default value is "Mappings.config".
		/// </summary>
		[ConfigurationProperty("mappingsFileName", DefaultValue = "Mappings.config", IsRequired = false)]
		public string MappingsFileName
		{
			get { return (string)this["mappingsFileName"]; }
			set { this["mappingsFileName"] = value; }
		}

		/// <summary>
		/// The path to the mappings file. If <see cref="MappingsFileSource" /> is set to 
		/// <c>mappingsFileSource.FileSystem</c>, this is the folder
		/// where the mappings file is located (either absolute or relative path). 
		/// If <see cref="MappingsFileSource" /> is set to <c>mappingsFileSource.Assembly</c>,
		/// this is the the name of the assembly that contains the mappings file as an embedded resource.
		/// </summary>
		[ConfigurationProperty("mappingsFileLocation", DefaultValue = @".\", IsRequired = false)]
		public string MappingsFileLocation
		{
			get { return (string)this["mappingsFileLocation"]; }
			set { this["mappingsFileLocation"] = value; }
		}

		/// <summary>
		/// Specifies where to find the mappings file, based on a <see cref="MappingsFileSource" /> value.
		/// The default value is <c>MappingsFileSource.FileSystem</c>.
		/// </summary>
		[ConfigurationProperty("mappingsFileSource", DefaultValue = MappingsFileSource.FileSystem, IsRequired = false)]
		public MappingsFileSource MappingsFileSource
		{
			get { return (MappingsFileSource)this["mappingsFileSource"]; }
			set { this["mappingsFileSource"] = value; }
		}

		/// <summary>
		/// The name of the connection string to use. Note that this is not the actual connection string 
		/// but the named connection string in the connectionStrings section of the configuration file. 
		/// The default value is "WilsonORWrapper".
		/// </summary>
		[ConfigurationProperty("connectionString", DefaultValue = "WilsonORWrapper", IsRequired = false)]
		public string ConnectionString
		{
			get { return (string)this["connectionString"]; }
			set { this["connectionString"] = value; }
		}

		/// <summary>
		/// The name of the logger to expose to the services. Logging to the event log and logging using
		/// the third-party tools Log4Net and NLog are supported. The default is no logging.
		/// </summary>
		[ConfigurationProperty("logger", DefaultValue = LoggerType.None, IsRequired = false)]
		public LoggerType Logger
		{
			get { return (LoggerType)this["logger"]; }
			set { this["logger"] = value; }
		}

		/// <summary>
		/// The name of the cache to expose to the services. Logging using the AspNet cache and the third-party
		/// Memcached library are supported. The default is no cache.
		/// </summary>
		[ConfigurationProperty("cache", DefaultValue = CacheType.None, IsRequired = false)]
		public CacheType Cache
		{
			get { return (CacheType)this["cache"]; }
			set { this["cache"] = value; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Initializes using the <c>wilsonorwrapper</c> section of the application's default
		/// configuration file.
		/// </summary>
		public static void Initialize()
		{
			lock (_settingsLock)
			{
				_settings = ConfigurationManager.GetSection("WilsonORWrapper") as ConfigurationSettings;
			}
		}
		#endregion
	}
}
