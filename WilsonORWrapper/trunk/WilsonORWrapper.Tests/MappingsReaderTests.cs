using System;
using WilsonORWrapper.Mappings;
using NUnit.Framework;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class MappingsReaderTests
	{
		[Test]
		public void GetFileSystemMappings()
		{
			IMappingsReader reader = new FileSystemMappingsReader();
			reader.GetMappingsFile("Mappings.config", AppDomain.CurrentDomain.BaseDirectory);
		}

		[Test]
		public void GetAssemblyMappings()
		{
			IMappingsReader reader = new AssemblyMappingsReader();
			reader.GetMappingsFile("Mappings.config", "WilsonORWrapper.Tests.dll");
		}
	}
}
