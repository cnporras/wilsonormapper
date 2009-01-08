using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Wilson.ORMapper.FluentMappings.ReflectionHelp;

namespace Wilson.ORMapper.FluentMappings
{
    public class MappedEntity<EntityType>
    {
        protected List<MappedAttribute> _attributes = new List<MappedAttribute>();
        public MappedAttribute[] Keys
        {
            get
            {
                var keys = this._attributes.FindAll( x=>x.IsKey = true);
                return keys.ToArray();
            }
        }

        /// <summary>
        /// Sets optional stored procedure names for insert, updating and deleting
        /// rather than using dynamic sql
        /// </summary>
        public StoredProcedures StoredProcedures { get; private set; }

        public MappedAttribute[] AllAttributes { get { return _attributes.ToArray();  } }

        public string TypeName { get; private set; }
        public string TableName { get; private set; }
        public Type ClassType
        {
            get { return typeof (EntityType); }
        }
        public KeyType KeyType
        {
            get; private set;
        }

        public MappedEntity()
        {
            this.TypeName = this.ClassType.Namespace + "." + this.ClassType.Name;
            this.TableName = this.ClassType.Name;
            this.StoredProcedures = new StoredProcedures();
        }

        /// <summary>
        /// maps an attribute (property or field) of an object to a database column
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public MappedAttribute Attribute(Expression<Func<EntityType, object>> property)
        {
            return AddAttribute(property, false);
        }

        private MappedAttribute AddAttribute(Expression<Func<EntityType, object>> property, bool isKey)
        {
            PropertyInfo propInfo = ReflectionHelper.GetProperty(property);
            

            var mappedAttr = new MappedAttribute(propInfo, isKey);
            this._attributes.Add(mappedAttr);
            

            return mappedAttr;            
        }

        /// <summary>
        /// Sets the unique key for the entity.  WORM uses this for tracking changes to objects.
        /// </summary>
        /// <param name="keyType"></param>
        /// <param name="properties"></param>
        public void Key(KeyType keyType, params Expression<Func<EntityType, object>>[] properties)
        {
            if (properties.Length == 0)
                throw new ArgumentException("Must pass properties to set the Key");
            if (keyType != KeyType.Composite && properties.Length > 1)
                throw new ArgumentException("If using multiple keys, the KeytType must be specified as composite!");

            this.KeyType = keyType;

           foreach(Expression<Func<EntityType, object>> property in properties )
           {
               this.AddAttribute(property, true);
           }
        }

        /// <summary>
        /// Sets a composite key
        /// </summary>
        /// <param name="properties"></param>
        public void CompositeKey(params Expression<Func<EntityType, object>>[] properties)
        {
            this.Key(KeyType.Composite, properties);
        }


        public void TheTableNameIs(string tablename)
        {
            this.TableName = tablename;
        }

    }
}
