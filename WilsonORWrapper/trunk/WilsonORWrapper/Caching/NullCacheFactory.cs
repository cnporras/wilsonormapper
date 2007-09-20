using System;

namespace WilsonORWrapper.Caching
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="NullCache"/>.
	/// </summary>
	public class NullCacheFactory : ICacheFactory
	{
		#region ICacheFactory Members
		/// <summary>
		/// Creates and returns an instance of <see cref="NullCache"/>.
		/// </summary>
		/// <returns>A <see cref="NullCache"/> object.</returns>
		public ICache CreateCache()
		{
			ICache cache = NullCache.Instance;

			return cache;
		}
		#endregion
	}
}
