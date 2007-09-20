using System;
using Memcached.ClientLibrary;

namespace WilsonORWrapper.Caching.Memcached
{
	/// <summary>
	/// Factory class that creates an instance of <see cref="MemcachedClient"/>, 
	/// a cache implementation for memcached (http://www.danga.com/memcached/) by Danga Interactive.
	/// The constructor will initialize the cache using provided configuration values, if any.
	/// Uses the C# API from https://sourceforge.net/projects/memcacheddotnet/ (version 1.1.4).
	/// </summary>
	public class MemcachedFactory : ICacheFactory
	{
		private string _instance;
		private string[] _serverList;

		#region Constructors
		/// <summary>
		/// Initializes the factory using the default instance name and server list.
		/// The default instance name is <c>WilsonORWrapper</c>.
		/// The default server list includes one server: <c>127.0.0.1:11211</c>.
		/// </summary>
		public MemcachedFactory()
			: this("WilsonORWrapper", "127.0.0.1:11211")
		{
		}
		/// <summary>
		/// Initializes the factory using the given instance name and the default server list.
		/// The default server list includes one server: <c>127.0.0.1:11211</c>.
		/// </summary>
		/// <param name="instance">The name of the instance.</param>
		public MemcachedFactory(string instance)
			: this(instance, "127.0.0.1:11211")
		{
		}
		/// <summary>
		/// Initializes the factory using the default instance name and the given server list.
		/// The default instance name is <c>WilsonORWrapper</c>.
		/// </summary>
		/// <param name="serverList">The address or names of the servers.</param>
		public MemcachedFactory(params string[] serverList)
			: this("WilsonORWrapper", serverList)
		{
		}
		/// <summary>
		/// Initializes the factory using the given instance name and server list.
		/// </summary>
		/// <param name="instance">The name of the instance.</param>
		/// <param name="serverList">The address or names of the servers.</param>
		public MemcachedFactory(string instance, params string[] serverList)
		{
			if (String.IsNullOrEmpty(instance))
				throw new ArgumentNullException("instance");
			if (serverList == null || serverList.Length == 0)
				throw new ArgumentException("At least one server must be provided in the serverList parameter.");

			_instance = instance;
			_serverList = serverList;

			SockIOPool pool = SockIOPool.GetInstance(instance);
			pool.SetServers(serverList);
			pool.Initialize();
		}
		#endregion

		#region ICacheFactory Members
		/// <summary>
		/// Creates an instance of <see cref="MemcachedCache"/> using default settings.
		/// </summary>
		/// <returns>The cache instance.</returns>
		public ICache CreateCache()
		{
			ICache cache = new MemcachedCache();

			return cache;
		}
		/// <summary>
		/// Creates an instance of <see cref="MemcachedCache"/> using the given pool name.
		/// </summary>
		/// <param name="poolName">The pool name.</param>
		/// <returns>The cache instance.</returns>
		public ICache CreateCache(string poolName)
		{
			ICache cache = new MemcachedCache(poolName);

			return cache;
		}
		/// <summary>
		/// Creates an instance of <see cref="MemcachedCache"/> with the given compression setting.
		/// </summary>
		/// <param name="enableCompression">The compression setting.</param>
		/// <returns>The cache instance.</returns>
		public ICache CreateCache(bool enableCompression)
		{
			ICache cache = new MemcachedCache(enableCompression);

			return cache;
		}
		/// <summary>
		/// Creates an instance of <see cref="MemcachedCache"/> with the given 
		/// pool name and compression setting.
		/// </summary>
		/// <param name="poolName">The pool name.</param>
		/// <param name="enableCompression">The compression setting.</param>
		/// <returns>The cache instance.</returns>
		public ICache CreateCache(string poolName, bool enableCompression)
		{
			ICache cache = new MemcachedCache(poolName, enableCompression);

			return cache;
		}
		#endregion
	}
}
