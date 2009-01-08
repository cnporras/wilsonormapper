using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Wilson.ORMapper.FluentMappings
{

    public class FluentMappingXmlTransformer
    {
        protected XmlDocument xml;
        protected XmlNode mappingNode;
        public const string DEFAULT_SCHMEA = "defaultSchema";
        public const string DEFAULT_HINT = "defaultHint";

        public FluentMappingXmlTransformer()
        {
            this.xml = new XmlDocument();
            this.mappingNode = this.xml.CreateNode(XmlNodeType.Element, "mapping", "");
            this.xml.AppendChild(mappingNode);            
        }

        public XmlDocument GetXmlMapping()
        {
            Console.WriteLine(xml.ToString());
            return this.xml;
        }

        private void SetAttribute(XmlNode node, string attribute, string value)
        {
            if (node.Attributes[attribute] == null)
                node.Attributes.Append(this.xml.CreateAttribute(attribute));

            node.Attributes[attribute].Value = value;

        }
    }
}
