using System;
using System.Globalization;
using System.IO;

namespace WilsonORWrapper.Mappings
{
	/// <summary>
	/// Implementation of <see cref="IMappingsReader" /> that reads the mappings file
	/// from the file system.
	/// </summary>
	public class FileSystemMappingsReader : IMappingsReader
	{
		private const string READER_EXCEPTION = "Unable to load mappings file from file system.";

		#region IMappingsReader Members
		/// <summary>
		/// Reads the mappings file from the file system
		/// and returns its contents in a <see cref="FileStream" />.
		/// </summary>
		/// <param name="name">The file name of the mappings file.</param>
		/// <param name="location">The relative or absolute path to the folder 
		/// that includes the file.</param>
		/// <returns>A <see cref="FileStream" /> pointing to the mappings file contents,
		/// or null if the file is not found.</returns>
		public Stream GetMappingsFile(string name, string location)
		{
			string fullname = FileSystemMappingsReader.ParseFileName(name, location);

			try
			{
				FileStream stream = new FileStream(fullname, FileMode.Open);
				if (stream == null)
				{
					throw new MappingsReaderException(READER_EXCEPTION);
				}

				return stream;
			}
			catch (Exception ex)
			{
				throw new MappingsReaderException(READER_EXCEPTION, ex);
			}
		}
		#endregion

		private static string ParseFileName(string filename, string path)
		{
			return String.Concat(path, (path.EndsWith("\\") ? String.Empty : "\\"), filename);
		}
	}
}
