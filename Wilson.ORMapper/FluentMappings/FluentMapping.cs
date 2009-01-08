using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Wilson.ORMapper.FluentMappings
{
    public class FluentMapping
    {

        public string DefaultSchema { get; private set; }
        public string DefaultHint { get; private set; }

        private List<MappedEntity<object>> Entities = new List<MappedEntity<object>>();

        public MappedEntity<object>[] AllEntities
        {
            get
            {
                return this.Entities.ToArray();
            }
        }
        


        public void TheDefaultSchemaIs(string schema)
        {
            this.DefaultSchema = schema;
        }

        public void TheDefaultHintIs(string hint)
        {
            this.DefaultHint = hint;
        }



        public void AddMappedEntity(MappedEntity<object> entity)
        {
            this.Entities.Add(entity);
        }

        public XmlDocument GetXmlMapping()
        {
            throw new NotImplementedException();
        }

    }
}
