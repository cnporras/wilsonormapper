using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Tests
{
	public abstract class AbstractCacheTests
	{
		private ICacheFactory factory;
		private ICache cache;

		public abstract ICacheFactory GetCacheFactory();

		[TestFixtureSetUp]
		public void SetUp()
		{
			factory = GetCacheFactory();
			cache = factory.CreateCache();
		}

		[Test]
		public void Set()
		{
			cache.Set("settest", "test data");
			Assert.AreEqual("test data", cache.Get("settest"));
		}
		[Test]
		public void Get()
		{
			cache.Set("gettest", "test data");

			object val = cache.Get("gettest");
			Assert.AreEqual("test data", (string)val);
			
			Assert.IsNull(cache.Get("doesnotexist"));
		}
		[Test]
		public void Contains()
		{
			cache.Set("containstest", "test data");

			Assert.IsTrue(cache.Contains("containstest"));
			Assert.IsFalse(cache.Contains("doesnotexist"));
		}
		[Test]
		public void Clear()
		{
			cache.Set("cleartest", "test data");

			cache.Clear();
			Assert.IsFalse(cache.Contains("cleartest"));
		}
	}
}
