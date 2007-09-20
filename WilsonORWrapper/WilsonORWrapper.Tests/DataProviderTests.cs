using System;
using System.Configuration;
using NUnit.Framework;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class DataProviderTests
	{
		[Test]
		public void GetObjectSpace()
		{
			Assert.IsNotNull(DataProvider.ObjectSpace);
		}

		[Test]
		public void ReadConnectionStringSettings()
		{
			ConnectionStringSettings settings = DataProvider.ConnectionStringSettings;
			Assert.IsNotNull(settings);
			Assert.AreEqual("TestData.xml", settings.ConnectionString);
			Assert.AreEqual("Wilson.XmlDbClient", settings.ProviderName);
		}
	}
}
