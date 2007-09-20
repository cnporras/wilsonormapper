using System;
using System.Collections;
using System.Globalization;
using System.Web;

namespace WilsonORWrapper.Caching
{
	/// <summary>
	/// Implements a cache interface that uses the <see cref="System.Web.Caching.Cache"/> implementation.
	/// </summary>
	public class AspNetCache : ICache
	{
		/// <summary>
		/// Initializes the cache object.
		/// </summary>
		public AspNetCache()
		{
		}

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
		/// Retrieves an object from the cache. If the ASP.Net cache is unavailable, this
		/// will always return null.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>The cached object.</returns>
		public object Get(object key)
		{
			if ( key == null)
				return null;

			HttpContext context = HttpContext.Current;
			if ( context == null )
				return null;

			return context.Cache[GetCacheKey(key)];
		}

		/// <summary>
		/// Places an item in the cache. If the ASP.Net cache is unavailable, this
		/// will do nothing.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		public void Set(object key, object value)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				context.Cache[GetCacheKey(key)] = value;
			}
		}

		/// <summary>
		/// Place an item in the cache that will expire after the given time span.
		/// If the ASP.Net cache is unavailable, this will do nothing.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="timeToExpire">The <see cref="TimeSpan"/> to use for
		/// item expiration.</param>
		public void Set(object key, object value, TimeSpan timeToExpire)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				context.Cache.Insert(GetCacheKey(key), value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, timeToExpire);
			}
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

			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				context.Cache.Insert(GetCacheKey(key), value, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
			}
		}

		/// <summary>
		/// Determines whether an object with the given key exists in the cache.
		/// If the ASP.Net cache is unavailable, this will always return false.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>True if the key exists in the cache, false otherwise.</returns>
		public bool Contains(object key)
		{
			if (key == null)
				return false;

			return ( this.Get(key) != null );
		}

		/// <summary>
		/// Removes an object from the cache. If the ASP.Net cache is unavailable, this
		/// will do nothing.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		public void Remove(object key)
		{
			if (key != null)
			{
				HttpContext context = HttpContext.Current;
				if (context != null)
				{
					context.Cache.Remove(GetCacheKey(key));
				}
			}
		}

		/// <summary>
		/// Removes all objects from the cache. If the ASP.Net cache is unavailable, this
		/// will do nothing.
		/// </summary>
		public void Clear()
		{
			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				foreach (DictionaryEntry item in context.Cache)
				{
					context.Cache.Remove(GetCacheKey(item.Key));
				}
			}
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
			if (key == null)
				throw new ArgumentNullException("key");

			return String.Concat(key.ToString(), ':', key.GetHashCode().ToString(CultureInfo.InvariantCulture));
		}
		#endregion
	}
}
