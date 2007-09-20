using System;
using NUnit.Framework;
using WilsonORWrapper.Caching.Memcached;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class MemcachedCacheTests : AbstractCacheTests
	{
		public override ICacheFactory GetCacheFactory()
		{
			return new MemcachedFactory();
		}
	}
}
