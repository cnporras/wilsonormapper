using System;
using WilsonORWrapper.Services;
using NUnit.Framework;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class EntityTests
	{
		[Test]
		public void ComparisonWithIIdentifiable()
		{
			Contact newcontact = new Contact();
			Contact existingcontact = Data<Contact>.RetrieveFirst();

			//compare to other == false
			Assert.IsFalse(newcontact.Equals(existingcontact));

			//compare to null == false
			Assert.IsFalse(newcontact.Equals(null));

			//compare to self == true
			Assert.IsTrue(newcontact.Equals(newcontact));

			//compare to another new object == true
			//it's true because autotrack is off and we didn't initialize key field, so both are default and equal
			Contact newcontact2 = new Contact();
			Assert.IsTrue(newcontact.Equals(newcontact2));

			//compare to the same contact loaded from the database == true
			Contact existingcontact2 = Data<Contact>.RetrieveFirst();
			Assert.IsTrue(existingcontact.Equals(existingcontact2));

			//compare to the same contact loaded from the database with changes to non-key field == true
			//this is true because Contact implements IIdentifiable, which uses IIdentity fields for comparison
			existingcontact2.FirstName = existingcontact2.FirstName + "!";
			Assert.IsTrue(existingcontact.Equals(existingcontact2));
		}

		[Test]
		public void ComparisonWithoutIIdentifiable()
		{
			Contact2 existingcontact = Data<Contact2>.RetrieveFirst();
			Contact2 existingcontact2 = Data<Contact2>.RetrieveFirst();

			//compare to the same contact loaded from the database twice == false
			//false due to different object references despite key field equality
			//implementing IIdentifiable should be used to work around this
			Assert.IsFalse(existingcontact.Equals(existingcontact2));
		}

	}
}
