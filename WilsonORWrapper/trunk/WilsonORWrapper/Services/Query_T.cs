using System;
using Wilson.ORMapper;
using WilsonORWrapper.Entities;

namespace WilsonORWrapper.Services
{
	/// <summary>
	/// Implementation for <see cref="Query{T}"/>.
	/// </summary>
	/// <typeparam name="T">An entity type.</typeparam>
	public class Query<T> where T : EntityBase<T>, new()
	{
		private string _where;
		private string _sortby;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Query()
		{
		}
		/// <summary>
		/// Constructs the query object using the given parameters.
		/// </summary>
		/// <param name="where">The where clause.</param>
		public Query(string where)
		{
			_where = where;
		}
		/// <summary>
		/// Constructs the query object using the given parameters.
		/// </summary>
		/// <param name="where">The where clause.</param>
		/// <param name="sortBy">The sort clause.</param>
		public Query(string where, string sortBy)
		{
			_where = where;
			_sortby = sortBy;
		}

		#region IQuery<T> Members
		/// <summary>
		/// The where clause. Use <see cref="String.Empty"/> for no criteria.
		/// </summary>
		public string Where
		{
			get { return _where; }
			set { _where = value; }
		}

		/// <summary>
		/// The sort clause. Use <see cref="String.Empty"/> for no sorting.
		/// </summary>
		public string SortBy
		{
			get { return _sortby; }
			set { _sortby = value; }
		}

		/// <summary>
		/// Returns an <see cref="OPathQuery{T}"/> object based on the given criteria.
		/// </summary>
		/// <returns>The <see cref="OPathQuery{T}"/> object.</returns>
		public OPathQuery<T> GetOPathQuery()
		{
			OPathQuery<T> query = new OPathQuery<T>(_where, _sortby);
			return query;
		}
		#endregion
	}
}
