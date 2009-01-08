using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Wilson.ORMapper.FluentMappings
{
    public class MappedAttribute
    {
 
        public string Field { get; set; }
        public string Member { get; internal set; }
        public bool IsKey { get; internal set; }

        public MappedAttribute(PropertyInfo prop, bool isKey)
        {
            
            this.Member = prop.Name;
            this.Field = this.Member;
            this.IsKey = isKey;
        }



        public void TheFieldNameIs(string fieldName)
        {
            this.Field = fieldName;
        }
    }
}
