using System;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Services
{
	/// <summary>
	/// Singleton class which provides caching functionality.
	/// </summary>
	public static class Cache
	{
		private static readonly ICache _cache = CacheProvider.Cache;

		#region ICache Members
		/// <summary>
		/// Retrieves an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>The cached object.</returns>
		public static object Get(object key)
		{
			return _cache.Get(key);
		}

		/// <summary>
		/// Places an item in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		public static void Set(object key, object value)
		{
			_cache.Set(key, value);
		}

		/// <summary>
		/// Place an item in the cache that will expire after the given time span.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="timeToExpire">The <see cref="TimeSpan"/> to use for
		/// item expiration.</param>
		public static void Set(object key, object value, TimeSpan timeToExpire)
		{
			_cache.Set(key, value, timeToExpire);
		}

		/// <summary>
		/// Place an item in the cache with the specified cache expiration.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="expiration">The <see cref="DateTime"/> to use for
		/// item expiration.</param>
		public static void Set(object key, object value, DateTime expiration)
		{
			_cache.Set(key, value, expiration);
		}

		/// <summary>
		/// Determines whether an object with the given key exists in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>True if the key exists in the cache, false otherwise.</returns>
		public static bool Contains(object key)
		{
			return _cache.Contains(key);
		}

		/// <summary>
		/// Removes an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		public static void Remove(object key)
		{
			_cache.Remove(key);
		}

		/// <summary>
		/// Removes all objects from the cache.
		/// </summary>
		public static void Clear()
		{
			_cache.Clear();
		}
		#endregion
	}
}
