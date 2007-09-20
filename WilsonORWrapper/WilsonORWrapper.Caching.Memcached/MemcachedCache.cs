using System;
using System.Globalization;
using Memcached.ClientLibrary;

namespace WilsonORWrapper.Caching.Memcached
{
	/// <summary>
	/// Implements a cache interface for memcached (http://www.danga.com/memcached/) by Danga Interactive.
	/// Uses the C# API from https://sourceforge.net/projects/memcacheddotnet/ (version 1.1.4).
	/// </summary>
	public class MemcachedCache : ICache
	{
		private MemcachedClient _client;

		#region Constructors
		/// <summary>
		/// Initializes the cache object using the default values: 
		/// <code>PoolName = "WilsonORWrapper"; 
		/// EnableCompression = false;</code>
		/// </summary>
		public MemcachedCache()
			: this("WilsonORWrapper", false)
		{
		}
		/// <summary>
		/// Initializes the cache object using the provided and default values: 
		/// <code>EnableCompression = false;</code>
		/// </summary>
		/// <param name="poolName">The name of the memcached pool.</param>
		public MemcachedCache(string poolName)
			: this(poolName, false)
		{
		}
		/// <summary>
		/// Initializes the cache object using the provided and default values: 
		/// <code>PoolName = "WilsonORWrapper";</code>
		/// </summary>
		/// <param name="enableCompression">True to enable compression, false otherwise.</param>
		public MemcachedCache(bool enableCompression)
			: this("WilsonORWrapper", enableCompression)
		{
		}
		/// <summary>
		/// Initializes the cache object using the provided values.
		/// </summary>
		/// <param name="poolName">The name of the memcached pool.</param>
		/// <param name="enableCompression">True to enable compression, false otherwise.</param>
		public MemcachedCache(string poolName, bool enableCompression)
		{
			_client = new MemcachedClient();
			_client.PoolName = poolName;
			_client.EnableCompression = enableCompression;
		}
		#endregion

		#region ICache Members
		/// <summary>
		/// Gets or sets the cache item with the specified key.
		/// </summary>
		/// <param name="key">The key of the cache item.</param>
		/// <returns>The cache item's value.</returns>
		public object this[object key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				this.Set(key, value);
			}
		}

		/// <summary>
		/// Retrieves an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>The cached object.</returns>
		public object Get(object key)
		{
			if ( key == null)
				return null;

			return _client.Get(GetCacheKey(key));
		}

		/// <summary>
		/// Places an item in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		public void Set(object key, object value)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			_client.Set(GetCacheKey(key), value);
		}

		/// <summary>
		/// Place an item in the cache that will expire after the given time span.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="timeToExpire">The <see cref="TimeSpan"/> to use for
		/// item expiration.</param>
		public void Set(object key, object value, TimeSpan timeToExpire)
		{
			Set(key, value, DateTime.Now.Add(timeToExpire));
		}

		/// <summary>
		/// Place an item in the cache with the specified cache expiration.
		/// If the ASP.Net cache is unavailable, this will do nothing.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="expiration">The <see cref="DateTime"/> to use for
		/// item expiration.</param>
		public void Set(object key, object value, DateTime expiration)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			_client.Set(GetCacheKey(key), value, expiration);
		}
		
		/// <summary>
		/// Determines whether an object with the given key exists in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>True if the key exists in the cache, false otherwise.</returns>
		public bool Contains(object key)
		{
			if (key == null)
				return false;

			return (_client.KeyExists(GetCacheKey(key)));
		}

		/// <summary>
		/// Removes an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		public void Remove(object key)
		{
			if (key != null)
			{
				_client.Delete(GetCacheKey(key));
			}
		}

		/// <summary>
		/// Removes all objects from the cache.
		/// </summary>
		public void Clear()
		{
			_client.FlushAll();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Creates a cache key based on the <paramref name="key"/> object's
		/// <code>ToString()</code> and <code>GetHashCode()</code> values.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>A formatted cache key.</returns>
		private static string GetCacheKey(object key)
		{
			return String.Concat(key.ToString(), ':', key.GetHashCode().ToString(CultureInfo.InvariantCulture));
		}
		#endregion
	}
}
