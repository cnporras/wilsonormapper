using System;
using NUnit.Framework;
using WilsonORWrapper.Providers;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class LogProviderTests
	{
		[Test]
		public void GetLogger()
		{
			Assert.IsNotNull(LogProvider.Logger);
		}
	}
}
