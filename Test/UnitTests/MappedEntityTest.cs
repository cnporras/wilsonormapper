using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wilson.ORMapper;
using Wilson.ORMapper.FluentMappings;

namespace Test
{
    [TestFixture]
    public class MappedEntityTest
    {
        [Test]
        public void Should_Have_Correct_Constructor_Defaults()
        {
            var entity = new MappedEntity<TestClass>();

            var entityName = typeof (TestClass).Namespace + "." + typeof (TestClass).Name;
            var table = typeof (TestClass).Name;
            Assert.AreEqual(entity.TypeName, entityName);
            Assert.AreEqual(entity.TableName, table);

        }

        [Test]
        public void Should_Allow_Different_Table()
        {
            var tableName = "SomeTable123";
            var entity = new MappedEntity<TestClass>();
            entity.TheTableNameIs(tableName);

            var entityName = typeof(TestClass).Namespace + "." + typeof(TestClass).Name;
            Assert.AreEqual(entity.TypeName, entityName);
            Assert.AreEqual(entity.TableName, tableName);
        }

        [Test]
        public void Should_Map_Attribute()
        {
            var entity = new MappedEntity<TestClass>();
            entity.Attribute(t => t.Title);
            entity.Attribute(t => t.SomeNumber);
            entity.Attribute(t => t.DifferentColumn).TheFieldNameIs("SomethingElse");


            Assert.AreEqual(entity.AllAttributes.Length, 3);
            var listOfAttributes = entity.AllAttributes.ToList();
            var titleAttr = listOfAttributes.Find(c => c.Member == "Title");
            Assert.IsNotNull(titleAttr);
            var SomeNumAttr = listOfAttributes.Find(c => c.Member == "SomeNumber");
            Assert.IsNotNull(SomeNumAttr);

            var differentOne = listOfAttributes.Find(c => c.Member == "DifferentColumn");
            Assert.IsNotNull(differentOne);
            Assert.AreEqual(differentOne.Field, "SomethingElse");
            Assert.AreEqual(differentOne.Member, "DifferentColumn");
        }

        

        [Test]
        public void Should_Take_SingleKey_Mapping()
        {
            var entity = new MappedEntity<TestClass>();

            var keyType = KeyType.User;
            entity.Key(keyType, x => x.ID);

            Assert.AreEqual(keyType, entity.KeyType);
            Assert.AreEqual(entity.Keys.Length, 1);
            Assert.AreEqual("ID", entity.Keys[0].Member);
           
        }

        [Test]
        public void Should_Take_Multiple_Keys_If_Composite()
        {
            var entity = new MappedEntity<TestClass>();

            var keyType = KeyType.Composite;
            
            entity.Key(keyType, 
                x => x.ID, 
                x => x.AltID);

            AssertCompositeKeys(entity, keyType);
        }

        private void AssertCompositeKeys(MappedEntity<TestClass> entity, KeyType keyType)
        {
            Assert.AreEqual(keyType, entity.KeyType);
            Assert.AreEqual(entity.Keys.Length, 2);
            Assert.AreEqual("ID", entity.Keys[0].Member);
            Assert.AreEqual("AltID", entity.Keys[1].Member);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Reject_Multiple_NonComposite_Keys(){
            

            var entity = new MappedEntity<TestClass>();

            var keyType = KeyType.Auto;

            entity.Key(keyType,
                x => x.ID,
                x => x.AltID);
            
        }

        [Test]
        public void Should_Accept_CompositeKey()
        {
            var entity = new MappedEntity<TestClass>();

            var keyType = KeyType.Composite;

            entity.CompositeKey(
                x => x.ID,
                x => x.AltID);            
        }

        [Test]
        public void Should_Take_Stored_Procs()
        {
            var entity = new MappedEntity<TestClass>();
            var insert = "usp_insert";
            var update = "usp_update";
            var delete = "usp_Delete";

            entity.StoredProcedures.Insert = insert;
            entity.StoredProcedures.Update = update;
            entity.StoredProcedures.Delete = delete;

            Assert.AreEqual(entity.StoredProcedures.Insert, insert);
            Assert.AreEqual(entity.StoredProcedures.Update, update);
            Assert.AreEqual(entity.StoredProcedures.Delete, delete);

            
        }

        public class TestClass
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string SomeNumber { get; set; }
            public string DifferentColumn { get; set; }
            public int AltID { get; set; }
        }
    }
}
