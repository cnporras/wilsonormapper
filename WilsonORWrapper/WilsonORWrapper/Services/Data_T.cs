using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Wilson.ORMapper;
using WilsonORWrapper.Entities;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Services
{
	///<summary>
	/// Service class which provides data access methods for entitites.persistence methods. 
	/// All methods are static. Can be inherited to provide strongly-typed, non-generic service classes.
	///</summary>
	///<typeparam name="T">The type of entity to persist.</typeparam>
	public class Data<T> : Data where T : EntityBase<T>, new()
	{
		#region Constructors
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected Data()
		{
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Parses the primary key where clause for a given <c>IIdentity</c> object.
		/// </summary>
		/// <param name="identity"></param>
		/// <returns></returns>
		protected static string GetPrimaryKeyWhereClause(IIdentity identity)
		{
			if (identity == null)
				return String.Empty;

			DictionaryEntry[] keys = identity.GetIdentityEntries();
			if (keys.Length == 0)
				return String.Empty;

			QueryHelper helper = DataProvider.ObjectSpace.QueryHelper;
			if (keys.Length == 1)
				return helper.GetExpression(keys[0].Key.ToString(), keys[0].Value);

			StringBuilder whereBuilder = new StringBuilder();
			for (int idx = 0; idx < keys.Length; idx++)
			{
				if (idx > 0)
				{
					whereBuilder.Append(" and ");
				}
				whereBuilder.Append(helper.GetExpression(keys[idx].Key.ToString(), keys[idx].Value));
			}

			return whereBuilder.ToString();
		}
		#endregion

		#region State
		/// <summary>
		/// Queries the instance and returns its <see cref="EntityState" />.
		/// </summary>
		/// <param name="instance">The instance to query.</param>
		/// <returns>The <see cref="EntityState" /> for the instance.</returns>
		public static EntityState GetState(T instance)
		{
			ObjectState state = GetObjectState(instance);
			switch (state)
			{
				case ObjectState.Inserted:
					return EntityState.New;
				case ObjectState.Unchanged:
					return EntityState.Unchanged;
				case ObjectState.Updated:
					return EntityState.Changed;
				case ObjectState.Deleted:
					return EntityState.Deleted;
				case ObjectState.Unknown:
					return EntityState.Unknown;
			}
			//otherwise throw an error
			throw new DataProviderException("Unable to determine entity state for instance");
		}
		/// <summary>
		/// Queries the instance to determine if it is new (i.e. has no valid persistable identity).
		/// </summary>
		/// <param name="instance">The instance to query.</param>
		/// <returns>True if the instance is new, false otherwise.</returns>
		public static bool IsNew(T instance)
		{
			ObjectState state = GetObjectState(instance);
			return (state == ObjectState.Inserted);
		}
		/// <summary>
		/// Queries the instance to determine if it is changed (i.e. has previously been persisted
		/// and has subsequent changes).
		/// </summary>
		/// <param name="instance">The instance to query.</param>
		/// <returns>True if the instance is changed, false otherwise.</returns>
		public static bool IsChanged(T instance)
		{
			ObjectState state = GetObjectState(instance);
			return (state == ObjectState.Updated);
		}
		/// <summary>
		/// Queries the instance to determine if it is deleted (i.e. its identity is no longer valid).
		/// </summary>
		/// <param name="instance">The instance to query.</param>
		/// <returns>True if the instance is deleted, false otherwise.</returns>
		public static bool IsDeleted(T instance)
		{
			ObjectState state = GetObjectState(instance);
			return (state == ObjectState.Deleted);
		}
		/// <summary>
		/// Queries the instance to determine if it is a valid O/R mapper entity.
		/// </summary>
		/// <param name="instance">The instance to query.</param>
		/// <returns>True if the instance is an entity, false otherwise.</returns>
		public static bool IsEntity(T instance)
		{
			ObjectState state = GetObjectState(instance);
			return (state != ObjectState.Unknown);
		}

		private static ObjectState GetObjectState(T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			return DataProvider.ObjectSpace.GetObjectState(instance);
		}
		#endregion

		#region Create/Track
		/// <summary>
		/// Initializes a new entity object and begins O/R mapper tracking.
		/// </summary>
		/// <returns>The object created.</returns>
		public static T Create()
		{
			T obj = new T();
			DataProvider.ObjectSpace.StartTracking(obj, InitialState.Inserted);

			return obj;
		}
		/// <summary>
		/// Enables O/R mapper tracking on an existing entity object.
		/// </summary>
		/// <param name="obj">The object to track.</param>
		public static void Track(T obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");

			DataProvider.ObjectSpace.StartTracking(obj, InitialState.Inserted);
		}
		#endregion

		#region Inner Retrieve Methods
		private static T InnerRetrieveOne(string whereClause, string sortClause, bool useCache)
		{
			ObjectQuery<T> query = new ObjectQuery<T>(whereClause, sortClause, 1, 1, true);
			ObjectSet<T> pageSet = InnerRetrieveMany(query, false);

			if (pageSet != null && pageSet.Count > 0)
				return pageSet[0];
			else
				return null;
		}
		private static T InnerRetrieveOne(OPathQuery<T> opathQuery, bool useCache, params object[] parameters)
		{
			if (useCache)
			{
				throw new NotImplementedException("Caching not implemented.");
			}
			else
			{
				return DataProvider.ObjectSpace.GetObject<T>(opathQuery, parameters);
			}
		}
		private static ObjectSet<T> InnerRetrieveMany(string whereClause, string sortClause, bool useCache)
		{
			ObjectQuery<T> query = new ObjectQuery<T>(whereClause, sortClause);

			return InnerRetrieveMany(query, useCache);
		}
		private static ObjectSet<T> InnerRetrieveMany(ObjectQuery<T> query, bool useCache)
		{
			if (useCache)
			{
				throw new NotImplementedException("Caching not implemented.");
			}
			else
			{
				return DataProvider.ObjectSpace.GetObjectSet<T>(query);
			}
		}
		private static ObjectSet<T> InnerRetrieveMany(OPathQuery<T> query, bool useCache, params object[] parameters)
		{
			if (useCache)
			{
				throw new NotImplementedException("Caching not implemented.");
			}
			else
			{
				return DataProvider.ObjectSpace.GetObjectSet<T>(query, parameters);
			}
		}
		private static ObjectSet<T> InnerRetrieveMany(CompiledQuery<T> query, bool useCache, params object[] parameters)
		{
			if (useCache)
			{
				throw new NotImplementedException("Caching not implemented.");
			}
			else
			{
				return DataProvider.ObjectSpace.GetObjectSet<T>(query, parameters);
			}
		}
		private static ObjectSet<T> InnerRetrievePage(string whereClause, string sortClause, int pageSize, int pageIndex, bool useCache)
		{
			if (DataProvider.ConnectionStringSettings.ProviderName == "System.Data.SqlClient" && String.IsNullOrEmpty(sortClause))
			{
				sortClause = "CURRENT_TIMESTAMP";
			}

			ObjectQuery<T> query = new ObjectQuery<T>(whereClause, sortClause, pageSize, pageIndex, true);

			return InnerRetrievePage(query, useCache);
		}
		private static ObjectSet<T> InnerRetrievePage(int startRowIndex, int maximumRows, string whereClause, string sortClause, bool useCache)
		{
			int pageIndex = (int)Math.Ceiling((double)startRowIndex / maximumRows) + 1;
			ObjectQuery<T> query = new ObjectQuery<T>(whereClause, sortClause, maximumRows, pageIndex, true);

			return InnerRetrievePage(query, useCache);
		}
		private static ObjectSet<T> InnerRetrievePage(ObjectQuery<T> query, bool useCache)
		{
			if (useCache)
				throw new NotImplementedException("Caching not implemented.");

			return DataProvider.ObjectSpace.GetObjectSet<T>(query);
		}
		#endregion

		#region Retrieve
		///<summary>Retrieves the object based on a matching <c>IIdentity</c>.</summary>
		///<param name="identity">The identity of the object to retrieve.</param>
		///<returns>The matching object, or null if not found.</returns>
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static T Retrieve(IIdentity identity)
		{
			if (identity == null)
				throw new ArgumentNullException("identity");

			string whereClause = GetPrimaryKeyWhereClause(identity);
			return InnerRetrieveOne(whereClause, String.Empty, false);
		}

		/// <summary>
		/// Retrieves all instances of this class based on a given <see cref="Query{T}"/>.
		/// </summary>
		/// <param name="query">The query to use.</param>
		/// <param name="parameters">The parameters to use.</param>
		/// <returns>The matching objects.</returns>
		public static Collection<T> Retrieve(Query<T> query, params object[] parameters)
		{
			if (query == null)
				throw new ArgumentNullException("query");

			return Retrieve(query.GetOPathQuery(), parameters);
		}

		///<summary>Retrieve all instances of this class from the persistence store.</summary>
		///<remarks>This method can be used for the <c>ObjectDataSource.SelectMethod</c>.</remarks>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static Collection<T> Retrieve()
		{
			return InnerRetrieveMany(String.Empty, String.Empty, false);
		}
		///<summary>Retrieves a collection of instances of this class from the persistence store 
		/// based on the where clause.</summary>
		///<param name="whereClause">The SQL where clause to filter the records.</param>
		public static Collection<T> Retrieve(string whereClause)
		{
			return InnerRetrieveMany(whereClause, String.Empty, false);
		}
		///<summary>Retrieves a sorted collection of instances of this class from the persistence store 
		/// based on the where clause.</summary>
		///<param name="whereClause">The SQL where clause to filter the records.</param>
		///<param name="sortClause">The SQL sort statement.</param>
		public static Collection<T> Retrieve(string whereClause, string sortClause)
		{
			return InnerRetrieveMany(whereClause, sortClause, false);
		}
		///<summary>Retrieve instances of this class from the persistence store based on the ObjectQuery</summary>
		///<param name="objectQuery">The object query to filter the records</param>
		public static Collection<T> Retrieve(ObjectQuery<T> objectQuery)
		{
			return InnerRetrieveMany(objectQuery, false);
		}
		/// <summary>Executes an OPathQuery against the data store and returns an collection filled with the results.</summary>
		/// <param name="opathQuery">OPathQuery to execute.</param>
		/// <returns>An IList filled with objects retrieved from the data store.</returns>
		public static Collection<T> Retrieve(OPathQuery<T> opathQuery)
		{
			return InnerRetrieveMany(opathQuery, false);
		}
		/// <summary>Executes an OPathQuery against the database and returns a collection filled with the results.</summary>
		/// <param name="opathQuery">OPathQuery to execute.</param>
		/// <param name="parameters">Parameter values to use when executing the query.</param>
		/// <returns>An IList filled with objects retrieved from the data store.</returns>
		public static Collection<T> Retrieve(OPathQuery<T> opathQuery, params object[] parameters)
		{
			return InnerRetrieveMany(opathQuery, false, parameters);
		}
		/// <summary>Executes a CompiledQuery against the database and returns an collection filled with the results.</summary>
		/// <param name="compiledQuery">CompiledQuery to execute.</param>
		/// <returns>An IList filled with objects retrieved from the data store.</returns>
		public static Collection<T> Retrieve(CompiledQuery<T> compiledQuery)
		{
			return InnerRetrieveMany(compiledQuery, false);
		}
		/// <summary>Executes a CompiledQuery against the database and returns a collection filled with the results.</summary>
		/// <param name="compiledQuery">CompiledQuery to execute.</param>
		/// <param name="parameters">Parameter values to use when executing the query.</param>
		/// <returns>An IList filled with objects retrieved from the data store.</returns>
		public static Collection<T> Retrieve(CompiledQuery<T> compiledQuery, params object[] parameters)
		{
			return InnerRetrieveMany(compiledQuery, false, parameters);
		}
		#endregion

		#region RetrieveFirst
		///<summary>Retrieve the first instance of this class.</summary>
		public static T RetrieveFirst()
		{
			return InnerRetrieveOne(String.Empty, String.Empty, false);
		}
		///<summary>Retrieve the first instance of this class using the where clause.</summary>
		///<param name="whereClause">The SQL where clause to filter the records</param>
		public static T RetrieveFirst(string whereClause)
		{
			return InnerRetrieveOne(whereClause, String.Empty, false);
		}
		///<summary>Retrieve the first instance of this class using the where clause.</summary>
		///<param name="whereClause">The SQL where clause to filter the records</param>
		///<param name="sortClause">The SQL sort statement</param>
		public static T RetrieveFirst(string whereClause, string sortClause)
		{
			return InnerRetrieveOne(whereClause, sortClause, false);
		}
		/// <summary>
		/// Retrieves the first instance of this class based on a given <see cref="Query{T}"/>.
		/// </summary>
		/// <param name="query">The query to use.</param>
		/// <param name="parameters">The parameters to use.</param>
		/// <returns>The matching object.</returns>
		public static T RetrieveFirst(Query<T> query, params object[] parameters)
		{
			if (query == null)
				throw new ArgumentNullException("query");

			return InnerRetrieveOne(query.GetOPathQuery(), false, parameters);
		}
		/// <summary>Retrieve the first instance of this class using an OPathQuery.</summary>
		/// <param name="query">OPathQuery to execute.</param>
		/// <param name="parameters">Parameter values to use when executing the query.</param>
		public static T RetrieveFirst(OPathQuery<T> query, params object[] parameters)
		{
			return InnerRetrieveOne(query, false, parameters);
		}
		#endregion

		#region RetrievePage
		/// <summary>Retrieve a paged collection of instances of this class from the persistence store.</summary>
		/// <param name="startRowIndex">Start index of the rows to retrieve.</param>
		/// <param name="maximumRows">The maximum rows.</param>
		/// <returns>Collection of items</returns>
		/// <remarks>
		/// <para>
		///     This method can be used for the <c>ObjectDataSource.SelectMethod</c> when 
		///     <c>ObjectDataSource.EnablePaging</c> is true. 
		/// </para>
		/// <list type="table">
		///     <listheader>
		///         <term>Property</term>
		///         <description>Value</description>
		///     </listheader>
		///     <item>
		///         <term><c>ObjectDataSource.StartRowIndexParameterName</c></term>
		///         <description>startRowIndex</description>
		///     </item>
		///     <item>
		///         <term><c>ObjectDataSource.MaximumRowsParameterName</c></term>
		///         <description>maximumRows</description>
		///     </item>
		/// </list>
		/// </remarks>
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static Collection<T> RetrievePage(int startRowIndex, int maximumRows)
		{
			return RetrievePage(startRowIndex, maximumRows, String.Empty, String.Empty);
		}
		/// <summary>Retrieve a paged collection of instances of this class from the persistence store.</summary>
		/// <param name="startRowIndex">Start index of the rows to retrieve.</param>
		/// <param name="maximumRows">The maximum rows.</param>
		/// <param name="sortClause">The SQL sort statement</param>
		/// <returns>Collection of items</returns>
		/// <remarks>
		/// <para>
		///     This method can be used for the <c>ObjectDataSource.SelectMethod</c> when 
		///     <c>ObjectDataSource.EnablePaging</c> is true. 
		/// </para>
		/// <list type="table">
		///     <listheader>
		///         <term>Property</term>
		///         <description>Value</description>
		///     </listheader>
		///     <item>
		///         <term><c>ObjectDataSource.StartRowIndexParameterName</c></term>
		///         <description>startRowIndex</description>
		///     </item>
		///     <item>
		///         <term><c>ObjectDataSource.MaximumRowsParameterName</c></term>
		///         <description>maximumRows</description>
		///     </item>
		///     <item>
		///         <term><c>ObjectDataSource.SortParameterName</c></term>
		///         <description>sortClause</description>
		///     </item>
		/// </list>
		/// </remarks>
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static Collection<T> RetrievePage(int startRowIndex, int maximumRows, string sortClause)
		{
			return RetrievePage(startRowIndex, maximumRows, String.Empty, sortClause);
		}
		/// <summary>Retrieve a paged collection of instances of this class from the persistence store.</summary>
		/// <param name="startRowIndex">Start index of the rows to retrieve.</param>
		/// <param name="maximumRows">The maximum rows.</param>
		/// <param name="whereClause">The SQL where statement</param>
		/// <param name="sortClause">The SQL sort statement</param>
		/// <returns>Collection of items</returns>
		/// <remarks>
		/// <para>
		///     This method can be used for the <c>ObjectDataSource.SelectMethod</c> when 
		///     <c>ObjectDataSource.EnablePaging</c> is true. 
		/// </para>
		/// <list type="table">
		///     <listheader>
		///         <term>Property</term>
		///         <description>Value</description>
		///     </listheader>
		///     <item>
		///         <term><c>ObjectDataSource.StartRowIndexParameterName</c></term>
		///         <description>startRowIndex</description>
		///     </item>
		///     <item>
		///         <term><c>ObjectDataSource.MaximumRowsParameterName</c></term>
		///         <description>maximumRows</description>
		///     </item>
		///     <item>
		///         <term><c>ObjectDataSource.SortParameterName</c></term>
		///         <description>sortClause</description>
		///     </item>
		/// </list>
		/// </remarks>
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static Collection<T> RetrievePage(int startRowIndex, int maximumRows, string whereClause, string sortClause)
		{
			int pageIndex = (int)Math.Ceiling((double)startRowIndex / maximumRows) + 1;
			ObjectQuery<T> query = new ObjectQuery<T>(whereClause, sortClause, maximumRows, pageIndex, true);
			return InnerRetrievePage(query, false);
		}
		///<summary>Retrieve a paged collection of instances of this class from the persistence store</summary>
		///<param name="sortClause">The SQL sort clause to order the records</param>
		///<param name="pageSize">The number of records in each page</param>
		///<param name="pageIndex">The page index to return</param>
		public static Collection<T> RetrievePage(string sortClause, int pageSize, int pageIndex)
		{
			return RetrievePage(String.Empty, sortClause, pageSize, pageIndex);
		}
		///<summary>Retrieve a paged collection of instances of this class from the persistence store</summary>
		///<param name="whereClause">The SQL where clause to filter the records</param>
		///<param name="sortClause">The SQL sort statement</param>
		///<param name="pageSize">The number of records in each page</param>
		///<param name="pageIndex">The page index to return</param>
		public static Collection<T> RetrievePage(string whereClause, string sortClause, int pageSize, int pageIndex)
		{
			return InnerRetrievePage(whereClause, sortClause, pageSize, pageIndex, false);
		}
		#endregion

		/*
		#region RetrieveCachedQuery
		///<summary>Retrieve instances of this class from the persistence store based on the ObjectQuery.</summary>
		///<remarks>This method uses the SqlCacheDependency to cache the results of the query.</remarks>
		///<param name="query">The object query to filter the records</param>
		public static ObjectSet<T> RetrieveCachedQuery(ObjectQuery<T> query)
		{
			System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
			if (cache == null)
			{
				return RetrieveQuery(query);
			}

			string key = string.Format("{0}|{1}|{2}|{3}",
				query.ObjectType.Name, query.WhereClause, query.SortClause, query.PageIndex);

			ObjectSet<T> cachedSet = cache[key] as ObjectSet<T>;
			if (cachedSet == null)
			{
				cachedSet = RetrieveQuery(query);
				string tableName = DataProvider.ObjectSpace.QueryHelper.GetTableName(typeof(T).Name);
				//TODO: Change as needed
				System.Web.Caching.SqlCacheDependency dependency = new System.Web.Caching.SqlCacheDependency(DataProvider.InitialCatalog, tableName);
				cache.Insert(key, cachedSet, dependency);
			}

			return cachedSet;
		}
		#endregion
		*/

		#region Save
		///<summary>Saves an instance to the persistence store.</summary>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(T instance)
		{
			return Save(null, instance, false);
		}
		///<summary>Saves an instance to the persistence store.</summary>
		///<param name="instance">The instance to persist.</param>
		///<param name="includeChildren">Include changes to related child instances</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(T instance, bool includeChildren)
		{
			return Save(null, instance, includeChildren);
		}
		///<summary>Saves an instance to the persistence store using a transaction.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="instance">The instance to persist.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(Transaction transaction, T instance)
		{
			return Save(transaction, instance, false);
		}
		///<summary>Saves an instance to the persistence store using a transaction.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="instance">The instance to persist.</param>
		///<param name="includeChildren">Include changes to related child instances.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(Transaction transaction, T instance, bool includeChildren)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");
			if (DataProvider.ObjectSpace.GetObjectState(instance) == ObjectState.Unknown)
				throw new DataProviderException("Attempt to save an instance which is not being tracked.");

			return InnerSave(transaction, instance, includeChildren);
		}

		///<summary>Saves a collection of instances to the persistence store.</summary>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ICollection instances)
		{
			return Save(null, instances, false);
		}
		///<summary>Saves a collection of instances to the persistence store.</summary>
		///<param name="instances">The collection of instances to persist.</param>
		///<param name="includeChildren">Include changes to related child instances</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(ICollection instances, bool includeChildren)
		{
			return Save(null, instances, includeChildren);
		}
		///<summary>Saves a collection of instances to the persistence store using a transaction.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="instances">The collection of instances to persist.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(Transaction transaction, ICollection instances)
		{
			return Save(transaction, instances, false);
		}
		///<summary>Saves a collection of instances to the persistence store using a transaction.
		///Any null or read-only objects in the collection will result in an error.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="instances">The collection of instances to persist.</param>
		///<param name="includeChildren">Include changes to related child instances.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.UpdateMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static int Save(Transaction transaction, ICollection instances, bool includeChildren)
		{
			if (instances == null)
				throw new ArgumentNullException("instances");

			foreach (T instance in instances)
			{
				if (instance == null)
					throw new DataProviderException("Attempt to save a null instance.");
				if (instance.IsReadOnly)
					throw new DataProviderException("Attempt to save a read-only instance.");
				if (DataProvider.ObjectSpace.GetObjectState(instance) == ObjectState.Unknown)
					throw new DataProviderException("Attempt to save an instance which is not being tracked.");
			}
		
			/* REFACTOR TO PERFORM "SMART SAVE" -- NEW OBJECTS INSERTED, EXISTING OBJECTS UPDATED */

			return InnerSave(transaction, instances, includeChildren);
		}

		private static int InnerSave(Transaction transaction, T instance, bool includeChildren)
		{
			if (instance.IsReadOnly)
				throw new DataProviderException("Can not save read-only instances.");

			PersistDepth depth = (includeChildren ? PersistDepth.ObjectGraph : PersistDepth.SingleObject);
			if (transaction == null)
			{
				DataProvider.ObjectSpace.PersistChanges(instance, depth);
			}
			else
			{
				transaction.PersistChanges(instance, depth);
			}

			return 1;
		}
		private static int InnerSave(Transaction transaction, ICollection instances, bool includeChildren)
		{
			PersistDepth depth = (includeChildren ? PersistDepth.ObjectGraph : PersistDepth.SingleObject);
			if (transaction == null)
			{
				DataProvider.ObjectSpace.PersistChanges(instances, depth);
			}
			else
			{
				transaction.PersistChanges(instances, depth);
			}

			ICollection<T> coll = instances as ICollection<T>;
			return (coll != null ? coll.Count : -1);
		}
		#endregion

		#region Delete
		///<summary>Deletes the specified instance from the persistence store/</summary>
		///<param name="instance">The instance to delete.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.DeleteMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(T instance)
		{
			return Delete(null, instance);
		}
		///<summary>Deletes the specified instances from the persistence store/</summary>
		///<param name="instances">The instances to delete.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.DeleteMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ICollection instances)
		{
			return Delete(null, instances);
		}
		///<summary>Deletes the object based on a matching <c>IIdentity</c>.</summary>
		///<param name="identity">The identity of the object to retrieve.</param>
		///<returns>The return value.</returns>
		[DataObjectMethod(DataObjectMethodType.Delete)]
		public static int Delete(IIdentity identity)
		{
			return Delete(null, identity);
		}
		///<summary>Delete instances from the persistence store based on the where clause</summary>
		///<param name="whereClause">The SQL where clause of rows to delete. Use String.Empty to delete all rows.</param>
		public static int Delete(string whereClause)
		{
			return InnerDelete(null, (whereClause == String.Empty ? "1=1" : whereClause));
		}
		///<summary>Delete an instance from the persistence store using a transaction</summary>
		///<param name="instance">The object to persist.</param>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		public static int Delete(Transaction transaction, T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");
			if (DataProvider.ObjectSpace.GetObjectState(instance) == ObjectState.Unknown)
				throw new DataProviderException("Attempt to delete an instance which is not being tracked.");
			if (instance.IsReadOnly)
				throw new DataProviderException("Can not delete read-only instances.");

			return InnerDelete(transaction, instance);
		}
		///<summary>Delete an instance from the persistence store using a transaction</summary>
		///<param name="instances">The object to persist.</param>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		public static int Delete(Transaction transaction, ICollection instances)
		{
			if (instances == null)
				throw new ArgumentNullException("instance");

			return InnerDelete(transaction, instances);
		}
		///<summary>Deletes the object based on a matching <c>IIdentity</c>.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="identity">The identity of the object to retrieve.</param>
		///<returns>The return value.</returns>
		[DataObjectMethod(DataObjectMethodType.Delete)]
		public static int Delete(Transaction transaction, IIdentity identity)
		{
			if (identity == null)
				throw new ArgumentNullException("identity");

			return InnerDelete(transaction, identity);
		}
		///<summary>Delete instances from the persistence store based on the where clause</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="whereClause">The SQL where clause of rows to delete</param>
		public static int Delete(Transaction transaction, string whereClause)
		{
			return InnerDelete(transaction, whereClause);
		}

		private static int InnerDelete(Transaction transaction, T instance)
		{
			DataProvider.ObjectSpace.MarkForDeletion(instance);

			if (transaction == null)
			{
				DataProvider.ObjectSpace.PersistChanges(instance);
			}
			else
			{
				transaction.PersistChanges(instance);
			}

			return 1;
		}
		private static int InnerDelete(Transaction transaction, ICollection instances)
		{
			foreach (T obj in instances)
			{
				InnerDelete(transaction, obj);
			}

			return instances.Count;
		}
		private static int InnerDelete(Transaction transaction, IIdentity identity)
		{
			string whereClause = GetPrimaryKeyWhereClause(identity);
			return InnerDelete(transaction, whereClause);
		}
		private static int InnerDelete(Transaction transaction, string whereClause)
		{
			if (transaction == null)
			{
				return DataProvider.ObjectSpace.ExecuteDelete(typeof(T), whereClause);
			}
			else
			{
				return transaction.ExecuteDelete(typeof(T), whereClause);
			}
		}
		#endregion

		#region Insert
		///<summary>Inserts the specified instance.</summary>
		///<param name="instance">The instance to insert.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.InsertMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public static int Insert(T instance)
		{
			return Insert(null, instance);
		}
		///<summary>Inserts the specified instance.</summary>
		///<param name="transaction">An instance of a Wilson.ORMapper.Transaction to perform operation with.</param>
		///<param name="instance">The instance to insert.</param>
		///<remarks>This method can be used for the <c>ObjectDataSource.InsertMethod</c>.</remarks>
		///<returns>The number of affected rows.</returns>
		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public static int Insert(Transaction transaction, T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (!Data<T>.IsNew(instance))
				throw new DataProviderException("Can not insert an object whose EntityState is not New.");

			return InnerSave(transaction, instance, false);
		}
		#endregion

		#region Other
		/// <summary>Refresh the data for this instance from the persistence store</summary>
		/// <returns>Returns a new instance with the refreshed data or null if instance not tracked</returns>
		/// <example>Resync an instance code fragment
		/// <code>
		/// Task instance;
		/// // Some retrieval and update logic
		/// instance = instance.Resync();
		/// </code>
		/// </example>
		public static T Resync(object obj)
		{
			if (DataProvider.ObjectSpace.GetObjectState(obj) == ObjectState.Unknown)
				return default(T);

			return (T)DataProvider.ObjectSpace.Resync(obj);
		}

		/// <summary>Gets the total row count.</summary>
		/// <returns>The number of total rows</returns>
		/// <remarks>This method is used by the ObjectDataSource to get the number of rows for the GridView.</remarks>
		public static int GetObjectCount()
		{
			return InnerGetObjectCount(String.Empty);
		}
		/// <summary>Gets the total row count.</summary>
		/// <returns>The number of total rows</returns>
		/// <remarks>This method is used by the ObjectDataSource to get the number of rows for the GridView.</remarks>
		public static int GetObjectCount(int startRowIndex, int maximumRows, string sortClause)
		{
			return InnerGetObjectCount(String.Empty);
		}
		/// <summary>Gets the total row count.</summary>
		/// <returns>The number of total rows</returns>
		/// <remarks>This method is used by the ObjectDataSource to get the number of rows for the GridView.</remarks>
		public static int GetObjectCount(int startRowIndex, int maximumRows, string whereClause, string sortClause)
		{
			return InnerGetObjectCount(whereClause);
		}
        private static int InnerGetObjectCount(string whereClause)
        {
            return DataProvider.ObjectSpace.GetObjectCount<T>(String.Empty);
        }
		#endregion

		#region Comparison Members
		/// <summary>
		/// Returns a numeric value representing the comparative value of the two object's
		/// underlying types and public property values.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare</param>
		/// <returns>Zero if the objects are of the same type and have equal public properties,
		/// or if both objects are null.
		/// In all other circumstances, a non-zero number is returned.</returns>
		public static int Compare(T x, T y)
		{
			if (x == null && y == null)
				return 0;

			if (x == null || y == null)
				return -1;

			Type type = typeof(T);
			PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public);
			FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
			int compareValue = 0;

			foreach (PropertyInfo property in properties)
			{
				IComparable valx = property.GetValue(x, null) as IComparable;
				if (valx == null)
					continue;
				object valy = property.GetValue(y, null);
				compareValue = valx.CompareTo(valy);
				if (compareValue != 0)
					return compareValue;
			}
			foreach (FieldInfo field in fields)
			{
				IComparable valx = field.GetValue(x) as IComparable;
				if (valx == null)
					continue;
				object valy = field.GetValue(y);
				compareValue = valx.CompareTo(valy);
				if (compareValue != 0)
					return compareValue;
			}

			return compareValue;
		}
		#endregion
	}
}
