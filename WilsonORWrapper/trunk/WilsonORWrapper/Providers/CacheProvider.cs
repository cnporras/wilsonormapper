using System;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace WilsonORWrapper.Providers
{
	/// <summary>
	/// Singleton class which exposes internal classes and methods for interacting
	/// with the cache interface.
	/// </summary>
	public static class CacheProvider
	{
		private static volatile ICache _cache;
		private static readonly Object _cacheLock = new Object();

		#region Properties
		/// <summary>
		/// Returns an <see cref="ICache"/> instance based on the configuration properties.
		/// This always returns a non-null cache client, and will always return the same instance
		/// of the same cache client.
		/// </summary>
		public static ICache Cache
		{
			get
			{
				if (_cache == null)
				{
					lock (_cacheLock)
					{
						if (_cache == null)
						{
							_cache = GetCache();
						}
					}
				}

				return _cache;
			}
		}
		#endregion

		#region Private Methods
		private static ICache GetCache()
		{
			CacheType cacheType = ConfigurationSettings.Settings.Cache;
			string assemblyName;
			string factoryName;

			switch (cacheType)
			{
				case CacheType.None:
					assemblyName = "WilsonORWrapper.dll";
					factoryName = "WilsonORWrapper.Caching.NullCacheFactory";
					break;
				case CacheType.AspNet:
					assemblyName = "WilsonORWrapper.dll";
					factoryName = "WilsonORWrapper.Caching.AspNetCacheFactory";
					break;
				case CacheType.Memcached:
					assemblyName = "WilsonORWrapper.Caching.Memcached.dll";
					factoryName = "WilsonORWrapper.Caching.Memcached.MemcachedFactory";
					break;
				default:
					throw new ConfigurationErrorsException("Specified Cache type in configuration file is unknown or invalid.");
			}

			if (System.Web.HttpContext.Current != null)
			{
				assemblyName = HttpRuntime.BinDirectory + assemblyName;
			}

			Assembly assembly = Assembly.LoadFrom(assemblyName);
			if (assembly == null)
				throw new CacheProviderException(assemblyName);

			ICacheFactory factory = assembly.CreateInstance(factoryName) as ICacheFactory;
			if (factory == null)
				throw new CacheProviderException(factoryName);

			ICache cache = factory.CreateCache();
			if (cache == null)
				throw new CacheProviderException("Unable to create cache");

			return cache;
		}
		#endregion
	}
}
