using System;
using NUnit.Framework;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class ConfigurationSettingsTests
	{
		[Test]
		public void ReadSettings()
		{
			Assert.IsNotNull(ConfigurationSettings.Settings, "ConfigurationSettings.Settings can not be null");
			ConfigurationSettings settings = ConfigurationSettings.Settings;

			Assert.AreEqual("Mappings.config", settings.MappingsFileName);
			Assert.AreEqual("WilsonORWrapper.Tests.dll", settings.MappingsFileLocation);
			Assert.AreEqual(MappingsFileSource.Assembly, settings.MappingsFileSource);
			Assert.AreEqual("WilsonORWrapper", settings.ConnectionString);
			Assert.AreEqual(LoggerType.None, settings.Logger);
		}
	}
}
