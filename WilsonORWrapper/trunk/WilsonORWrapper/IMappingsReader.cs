using System;
using System.IO;

namespace WilsonORWrapper
{
	/// <summary>
	/// Interface which exposes methods to be used for reading
	/// a mappings file from various locations.
	/// </summary>
	public interface IMappingsReader
	{
		/// <summary>
		/// Method which reads a mappings file 
		/// and returns its contents as a <see cref="Stream" />.
		/// </summary>
		/// <param name="name">The name of the mappings file.</param>
		/// <param name="location">The location of the mappings file. The actual
		/// use of this can vary based on the implementation..</param>
		/// <returns>A <see cref="Stream" /> pointing to the mappings file contents,
		/// or null if the file is not found.</returns>
		Stream GetMappingsFile(string name, string location);
	}
}
