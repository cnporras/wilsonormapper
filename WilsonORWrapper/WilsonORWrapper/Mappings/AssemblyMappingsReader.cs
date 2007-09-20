using System;
using System.IO;
using System.Reflection;

namespace WilsonORWrapper.Mappings
{
	/// <summary>
	/// Implementation of <see cref="IMappingsReader" /> that reads the mappings file
	/// stored as an embedded resource in an assembly.
	/// </summary>
	public class AssemblyMappingsReader : IMappingsReader
	{
		private const string READER_EXCEPTION = "Unable to load mappings file from assembly.";

		#region IMappingsReader Members
		/// <summary>
		/// Reads the mappings file from the assembly manifest of a given assembly
		/// and returns its contents as a <see cref="Stream" />.
		/// </summary>
		/// <param name="name">The file name of the mappings file.</param>
		/// <param name="location">The name of the assembly that includes the file.</param>
		/// <returns>A <see cref="Stream" /> pointing to the mappings file contents,
		/// or null if the assembly or mappings file resource is not found.</returns>
		public Stream GetMappingsFile(string name, string location)
		{
			try
			{
				Assembly assembly = Assembly.LoadFrom(location);
				Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + name);
				if (stream == null)
				{
					throw new MappingsReaderException(READER_EXCEPTION);
				}
				
				return stream;
			}
			catch ( Exception ex )
			{
				throw new MappingsReaderException(READER_EXCEPTION, ex);
			}
		}
		#endregion
	}
}
