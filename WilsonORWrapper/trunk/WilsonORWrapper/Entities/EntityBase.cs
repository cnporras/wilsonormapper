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
	/// <see cref="IComparable" />, <see cref="IInitializable" />, and
	/// <see cref="IIdentifiable" /> interfaces.
	/// </summary>
	/// <remarks>
	/// Child classes which are read-only should marked with the <see cref="ReadOnlyAttribute"/>.
	/// </remarks>
	public abstract class EntityBase : IComparable, IInitializable
	{
		private bool? _isReadOnly = null;

		#region Constructors
		/// <summary>
		/// The base constructor for all entities. This calls the <see cref="Initialize()" /> method exposed
		/// by the <see cref="IInitializable" /> interface. As a result, it is important for all child
		/// constructors to call the base constructor.
		/// If the entity has the <see cref="AutoTrackAttribute"/> set to true, the object will automatically
		/// be tracked by the O/R mapper.
		/// </summary>
		protected EntityBase()
		{
			Initialize();

			if (this.GetType().IsDefined(typeof(AutoTrackAttribute), true))
			{
				Data.Track(this);
			}
		}
		#endregion

		#region IComparable
		/// <summary>
		/// Compares this entity to another entity. If both objects implement
		/// the <see cref="IIdentifiable"/> interface, then comparison is based
		/// on each object's <see cref="IIdentity"/>. Otherwise, comparison
		/// is based on reference equality (handled by <see cref="Object.Equals(object)"/>.)
		/// </summary>
		/// <param name="obj">The entity to compare to.</param>
		/// <returns>A number reflecting the similarity of objects.
		/// A value of zero indicates equality; all other values indicate inequality.
		/// </returns>
		public int CompareTo(object obj)
		{
			if (obj == null)
				return -1;
			if (this == obj)
				return 0;

			IIdentifiable obj1 = this as IIdentifiable;
			IIdentifiable obj2 = obj as IIdentifiable;

			if (obj1 != null && obj2 != null)
				return obj1.GetIdentity().CompareTo(obj2.GetIdentity());

			return 1;
		}
		#endregion

		#region IInitializable
		/// <summary>
		/// Called during an object's construction, this method allows partial classes to
		/// interface with an object's construction without risk of editing partial class
		/// code files created by code generators.
		/// </summary>
		public virtual void Initialize()
		{
		}
		#endregion

		#region System.Object
		/// <summary>
		/// Indicates whether this entity is equal to another specified entity by examining
		/// the results of the <see cref="CompareTo"/> method.
		/// </summary>
		public override bool Equals(object obj)
		{
			return (this.CompareTo(obj) == 0 ? true : false);
		}
		/// <summary>
		/// Returns the hash code for this entity. If the object implements the
		/// <see cref="IIdentifiable"/> interface, the hash code of this object's
		/// <see cref="IIdentity"/> is returned; otherwise, the base 
		/// <see cref="GetHashCode"/> method is called.
		/// </summary>
		public override int GetHashCode()
		{
			if (this is IIdentifiable)
				return ((IIdentifiable)this).GetIdentity().GetHashCode();

			return base.GetHashCode();
		}
		#endregion

		#region SetProperties
		/// <summary>
		/// Allows setting properties using a dictionary (key/value pairs).
		/// The key coincides with the property name, and the value is the value to be set.
		/// </summary>
		/// <param name="properties">The dictionary containing key/value pairs.</param>
		public void SetProperties(IDictionary properties)
		{
			if (this.IsReadOnly)
				throw new InvalidOperationException("Object is read-only");

			foreach (object k in properties.Keys)
			{
				//k = property name; props[k] = property value
				PropertyInfo pi = this.GetType().GetProperty(k.ToString());
				if (pi == null)
					continue;

				//use reflection to determine the type of each property (e.g. string, int, datetime) and assign the value
				Type propertyType = pi.PropertyType;
				if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					// the property is a nullable field
					propertyType = Nullable.GetUnderlyingType(propertyType);
				}

				object propValue = null;
				if (properties[k] != null)
				{
					switch (propertyType.FullName.ToLower())
					{
						case "system.guid":
							propValue = new Guid((string)properties[k]);
							break;
						default:
							propValue = Convert.ChangeType(properties[k], propertyType);
							break;
					}
				}
				pi.SetValue(this, propValue, null);
			}
		}
		#endregion

		/// <summary>
		/// Returns a value specifying whether this entity is read-only or not.
		/// The value of this property is derived from the presence of the <see cref="ReadOnlyAttribute"/>
		/// on the class declaration. If <see cref="ReadOnlyAttribute"/> is not specified, the
		/// class is read/write; otherwise, the read/write state is based on the attribute.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				if (!_isReadOnly.HasValue)
				{
					object[] attribs = this.GetType().GetCustomAttributes(typeof(ReadOnlyAttribute), true);
					_isReadOnly = (attribs.Length == 0 ? false : ((ReadOnlyAttribute)attribs[0]).IsReadOnly);
				}

				return _isReadOnly.Value;
			}
			set
			{
				//do nothihng; this stub is here merely to permit databinding without the error:
				//  "The 'IsReadOnly' property on the type specified by the DataObjectTypeName property ...
				//  is readonly and its value cannot be set."
			}
		}
	}
}
