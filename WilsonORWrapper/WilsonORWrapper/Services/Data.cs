using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Wilson.ORMapper;
using WilsonORWrapper.Entities;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Services
{
	///<summary>
	/// Service class which provides data access methods of traditional SQL data.
	/// Serves as the base class to the generic <see cref="Data{T}"/> class.
	/// All methods are static.
	///</summary>
	public class Data
	{
		#region Constructors
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected Data()
		{
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Enables O/R mapper tracking on an existing entity object.
		/// </summary>
		/// <param name="obj">The object to track.</param>
		public static void Track(EntityBase obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");

			DataProvider.ObjectSpace.StartTracking(obj, InitialState.Inserted);
		}

		/// <summary>
		/// Executes a SQL command, returning a <see cref="DataSet"/>.
		/// </summary>
		/// <param name="sqlStatement">The SQL command to execute.</param>
		/// <returns>A <see cref="DataSet"/>.</returns>
		public static DataSet ExecuteDataSet(string sqlStatement)
		{
			return DataProvider.ObjectSpace.GetDataSet(sqlStatement);
		}

		/// <summary>
		/// Executes a SQL command, returning a single scalar value.
		/// </summary>
		/// <param name="sqlStatement">The SQL command to execute.</param>
		/// <returns>The scalar value.</returns>
		public static object ExecuteScalar(string sqlStatement)
		{
			return DataProvider.ObjectSpace.ExecuteScalar(sqlStatement);
		}
		#endregion

		/// <summary>
		/// Returns a numeric value representing the comparative value of the two object's
		/// underlying types and public property values.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare</param>
		/// <returns>Zero if the objects are of the same type and have equal public properties,
		/// or if both objects are null.
		/// In all other circumstances, a non-zero number is returned.</returns>
		public static int Compare(object x, object y)
		{
			if (x == null && y == null)
				return 0;
			
			if (x == null || y == null)
				return -1;

			Type type = x.GetType();
			if (!type.Equals(y.GetType()))
				return -1;

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
	}
}
