using System;
using NUnit.Framework;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class MappingsProviderTests
	{
		[Test]
		public void GetFileSystemMappingsFile()
		{
			Assert.IsNotNull(MappingsProvider.GetMappingsFile());
		}
	}
}
