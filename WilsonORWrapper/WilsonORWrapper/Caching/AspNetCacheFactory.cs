using System;

namespace WilsonORWrapper.Caching
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="AspNetCache"/>.
	/// </summary>
	public class AspNetCacheFactory : ICacheFactory
	{
		#region ICacheFactory Members
		/// <summary>
		/// Creates an instance of AspCetCache.
		/// </summary>
		/// <returns></returns>
		public ICache CreateCache()
		{
			ICache cache = new AspNetCache();

			return cache;
		}
		#endregion
	}
}
