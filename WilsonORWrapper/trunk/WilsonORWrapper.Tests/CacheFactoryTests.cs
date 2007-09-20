using System;
using NUnit.Framework;
using WilsonORWrapper.Caching;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class CacheFactoryTests
	{
		[Test]
		public void NullCacheTests()
		{
			ICacheFactory factory = new NullCacheFactory();
			Assert.IsNotNull(factory);
			ICache cache = factory.CreateCache();
			Assert.IsNotNull(cache);
		}

		[Test]
		public void AspNetCacheTests()
		{
			ICacheFactory factory = new AspNetCacheFactory();
			Assert.IsNotNull(factory);
			ICache cache = factory.CreateCache();
			Assert.IsNotNull(cache);
		}

	}
}
