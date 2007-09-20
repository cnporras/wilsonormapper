using System;

namespace WilsonORWrapper.Attributes
{
	/// <summary>
	/// Identifies whether a class should automatically be tracked by the O/R mapper
	/// on creation. Using this attribute on an entity will result in the entity
	/// being tracked at the time of construction.
	/// </summary>
	/// <remarks>
	/// If you use this attribute, any classes which have a user-defined primary key
	/// must set their primary key fields in the constructor, otherwise the underlying
	/// O/R mapper will throw an error (caused by a null key field).
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false), Serializable]
	public sealed class AutoTrackAttribute : Attribute
	{
		/// <summary>
		/// Sets automatic tracking for this class.
		/// </summary>
		public AutoTrackAttribute()
		{
		}
	}
}
