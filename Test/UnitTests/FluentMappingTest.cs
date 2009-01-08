using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MbUnit.Framework;
using Wilson.ORMapper.FluentMappings;

namespace Test
{
    [TestFixture]
    public class FluentMappingTest
    {
        protected FluentMapping map;

        [SetUp]
        public void Setup()
        {
            this.map = new FluentMapping();
        }


        [Test]
        public void Should_Set_Schema_And_Hint()
        {
            map.TheDefaultSchemaIs("dbo");
            map.TheDefaultHintIs("hint");

            Assert.AreEqual(map.DefaultSchema, "dbo");
            Assert.AreEqual(map.DefaultHint, "hint");
        }

        [Test]
        public void Should_Add_Entity()
        {
            MappedEntity<object> entityMap = new MappedEntity<object>();
            map.AddMappedEntity(entityMap);

            Assert.Greater(map.AllEntities.Length, 0);
        }

       
    }
}
