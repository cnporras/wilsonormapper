using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using WilsonORWrapper;
using WilsonORWrapper.Mappings;

namespace WilsonORWrapper.Providers
{
	/// <summary>
	/// Provides access to internal classes and methods for reading mappings files.
	/// </summary>
	public static class MappingsProvider
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a <see cref="System.IO.Stream" /> for the mappings file, based on configuration
		/// options in the application's <see cref="ConfigurationSettings" />.
		/// </summary>
		/// <returns>The mappings file stream.</returns>
		public static Stream GetMappingsFile()
		{
			IMappingsReader reader = GetMappingsReader();
			ConfigurationSettings settings = ConfigurationSettings.Settings;
			string name = settings.MappingsFileName;
			string location = settings.MappingsFileLocation;
			MappingsFileSource source = settings.MappingsFileSource;

			// Prefix the assembly name with bin directory for web apps
			if (source == MappingsFileSource.Assembly && !File.Exists(location))
			{
				if (HttpContext.Current != null)
				{
					location = Path.Combine(HttpRuntime.BinDirectory, location);
				}
				else 
				{
					string fileLoadedFrom = Assembly.GetExecutingAssembly().Location;
					string pathLoadedFrom = fileLoadedFrom.Substring(0, fileLoadedFrom.LastIndexOf("\\") + 1);
					location = Path.Combine(pathLoadedFrom, location);
				}
			}

			return reader.GetMappingsFile(name, location);
		}
		#endregion

		#region Private Methods
		private static IMappingsReader GetMappingsReader()
		{
			ConfigurationSettings settings = ConfigurationSettings.Settings;
			MappingsFileSource location = settings.MappingsFileSource;

			IMappingsReader reader;
			switch (location)
			{
				case MappingsFileSource.Assembly:
					reader = new AssemblyMappingsReader();
					break;
				case MappingsFileSource.FileSystem:
					reader = new FileSystemMappingsReader();
					break;
				default:
					throw new ConfigurationErrorsException("Specified Mappings File Source in configuration file is unknown or invalid.");
			}

			return reader;
		}
		#endregion
	}
}
