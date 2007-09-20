using System;

namespace WilsonORWrapper.Caching
{
	/// <summary>
	/// Singleton instance of a cache that does nothing.
	/// </summary>
	public class NullCache : ICache
	{
		private static readonly NullCache _instance = new NullCache();

		/// <summary>
		/// Retuns a singleton instance of <see cref="NullCache"/>.
		/// </summary>
		public static NullCache Instance
		{
			get { return _instance; }
		}

		#region Constructors
		/// <summary>
		/// Initializes the cache object.
		/// </summary>
		protected NullCache()
		{
		}
		#endregion

		#region ICache Members
		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <returns>null</returns>
		public object this[object key]
		{
			get
			{
				return null;
			}
			set
			{
				// do nothing
			}
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <returns>null</returns>
		public object Get(object key)
		{
			return null;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <param name="value">Doesn't matter.</param>
		public void Set(object key, object value)
		{
			// do nothing
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <param name="value">Doesn't matter.</param>
		/// <param name="timeToExpire">Doesn't matter.</param>
		public void Set(object key, object value, TimeSpan timeToExpire)
		{
			// do nothing
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <param name="value">Doesn't matter.</param>
		/// <param name="expiration">Doesn't matter.</param>
		public void Set(object key, object value, DateTime expiration)
		{
			// do nothing
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		/// <returns>false</returns>
		public bool Contains(object key)
		{
			return false;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="key">Doesn't matter.</param>
		public void Remove(object key)
		{
			// do nothing
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		public void Clear()
		{
			// do nothing
		}
		#endregion
	}
}
