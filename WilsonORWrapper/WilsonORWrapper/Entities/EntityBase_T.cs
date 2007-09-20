using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using WilsonORWrapper.Attributes;
using WilsonORWrapper.Services;

namespace WilsonORWrapper.Entities
{
	/// <summary>
	/// The base class for all O/R mapper entities. All entities must implement the
	/// <see cref="IComparable" />, <see cref="IComparable{T}" />, 
	/// <see cref="IEquatable{T}" />, and <see cref="IInitializable" />
	/// interfaces.
	/// </summary>
	/// <remarks>
	/// Child classes which are read-only should marked with the <see cref="ReadOnlyAttribute"/>.
	/// </remarks>
	/// <typeparam name="T">The entity object type (class name).</typeparam>
	public abstract class EntityBase<T> : EntityBase, IComparable<T>, IEquatable<T> where T : EntityBase<T>, new()
	{
		#region Constructors
		/// <summary>
		/// The base constructor for all strongly-typed entities. 
		/// This calls the base <see cref="EntityBase" /> class's constructor, which exposes
		/// functionality for the <see cref="IInitializable" /> interface. 
		/// As a result, it is important for all child constructors to call the base constructor.
		/// </summary>
		protected EntityBase()
			: base()
		{
		}
		#endregion

		#region IComparable
		/// <summary>
		/// Compares this entity to another entity. If both objects implement
		/// the <see cref="IIdentifiable"/> interface, then comparison is based
		/// on each object's <see cref="IIdentity"/>. Otherwise, comparison
		/// is based on reference equality.
		/// </summary>
		/// <param name="other">The entity to compare to.</param>
		/// <returns>A number reflecting the similarity of objects.
		/// A value of zero indicates equality; all other values indicate inequality.
		/// </returns>
		public int CompareTo(T other)
		{
			if (other == this)
				return 0;
			if (other == null)
				return -1;

			IIdentifiable obj1 = this as IIdentifiable;
			IIdentifiable obj2 = other as IIdentifiable;

			if (obj1 != null && obj2 != null)
				return obj1.GetIdentity().CompareTo(obj2.GetIdentity());

			return 1;
		}
		#endregion

		#region IEquatable
		/// <summary>
		/// Indicates whether this entity is equal to another specified entity by examining
		/// the results of the <see cref="CompareTo"/> method.
		/// </summary>
		/// <param name="other">The entity to compare to. Must implement <see cref="IIdentifiable" />.</param>
		/// <returns>True if the objects are equal, false otherwise.</returns>
		public bool Equals(T other)
		{
			return (this.CompareTo(other) == 0);
		}
		#endregion
	}
}
