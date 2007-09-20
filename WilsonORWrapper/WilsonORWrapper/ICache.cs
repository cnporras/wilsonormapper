using System;

namespace WilsonORWrapper
{
	/// <summary>
	/// Defines the implementation of a cache client.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Gets or sets the cache item with the specified key.
		/// Get calls should call the <see cref="Get"/> method.
		/// Set calls should call the <see cref="Set(object,object)"/> method.
		/// </summary>
		/// <param name="key">The key of the cache item.</param>
		/// <returns>The cache item's value.</returns>
		object this[object key] { get; set; }

		/// <summary>
		/// Retrieves an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>The cached object.</returns>
		object Get(object key);

		/// <summary>
		/// Places an item in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		void Set(object key, object value);

		/// <summary>
		/// Place an item in the cache that will expire after the given time span.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="timeToExpire">The <see cref="TimeSpan"/> to use for
		/// item expiration.</param>
		void Set(object key, object value, TimeSpan timeToExpire);

		/// <summary>
		/// Place an item in the cache with the specified cache expiration.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <param name="value">The cached object.</param>
		/// <param name="expiration">The <see cref="DateTime"/> to use for
		/// item expiration.</param>
		void Set(object key, object value, DateTime expiration);

		/// <summary>
		/// Determines whether an object with the given key exists in the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		/// <returns>True if the key exists in the cache, false otherwise.</returns>
		bool Contains(object key);

		/// <summary>
		/// Removes an object from the cache.
		/// </summary>
		/// <param name="key">The key of the object.</param>
		void Remove(object key);

		/// <summary>
		/// Removes all objects from the cache.
		/// </summary>
		void Clear();
	}
}
